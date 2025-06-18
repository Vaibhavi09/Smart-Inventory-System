using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechApi.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Azure;

namespace TechApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        [HttpPost]
        public ActionResult SaveInventoryData(Inventory InventoryDto)
        {
            using (SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=LAPTOP-QG4GJCJK;Database=techDb;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"
            })
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = "sp_SaveInventoryData",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                command.Parameters.AddWithValue("@ProductId", InventoryDto.ProductID);
                command.Parameters.AddWithValue("@ProductName", InventoryDto.ProductName);
                command.Parameters.AddWithValue("@StockAvailable", InventoryDto.StockAvailable);
                command.Parameters.AddWithValue("@ReorderStock", InventoryDto.ReorderStock);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            // Return a JSON response
            return Ok(new { message = "Inventory Data Saved!" });
        }


        [HttpDelete]
        public ActionResult DeleteInventoryData(int ProductId)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=LAPTOP-QG4GJCJK;Database=techDb;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"
            };

                SqlCommand command = new SqlCommand
                {
                    CommandText = "sp_DeleteInventoryDetails",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
              
                connection.Open();
                command.Parameters.AddWithValue("@ProductId", ProductId);
                command.ExecuteNonQuery();
                connection.Close();
           
            // Return a JSON response
            return Ok();
        }


        [HttpPut]
        public ActionResult UpdateInventoryData(Inventory InventoryDto)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=LAPTOP-QG4GJCJK;Database=techDb;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"
            };

            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_UpdateInventoryData",
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = connection
            };

            connection.Open();
            command.Parameters.AddWithValue("@ProductId", InventoryDto.ProductID);
            command.Parameters.AddWithValue("@ProductName", InventoryDto.ProductName);
            command.Parameters.AddWithValue("@StockAvailable", InventoryDto.StockAvailable);
            command.Parameters.AddWithValue("@ReorderStock", InventoryDto.ReorderStock);
            command.ExecuteNonQuery();
            connection.Close();

            // Return a JSON response
            return Ok();
        }



        [HttpGet]
        public ActionResult GetInventoryData()
        {
            using (SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=LAPTOP-QG4GJCJK;Database=techDb;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"
            })
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = "sp_GetInventoryData",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };

                connection.Open();

                List<InventoryDto> response = new List<InventoryDto>();

                using (SqlDataReader sqlDataReader = command.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        InventoryDto inventoryDto = new InventoryDto();
                        inventoryDto.ProductID = Convert.ToInt32(sqlDataReader["ProductId"]);
                        inventoryDto.ProductName = Convert.ToString(sqlDataReader["ProductName"]);
                        inventoryDto.StockAvailable = Convert.ToInt32(sqlDataReader["StockAvailable"]);
                        inventoryDto.ReorderStock = Convert.ToInt32(sqlDataReader["ReorderStock"]);

                        response.Add(inventoryDto);
                    }
                }

                connection.Close();
                // Fix: Use the class name 'JsonConvert' directly instead of an instance reference
                return Ok(JsonConvert.SerializeObject(response));
            }

        }
    }
}
