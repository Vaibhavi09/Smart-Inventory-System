using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechApi.Models;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace TechApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpPost]
        public ActionResult SaveCustomerData(CustomerRequestDto CustomerRequestDto)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=LAPTOP-QG4GJCJK;Database=techDb;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"
            };
            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_SaveCustomerDetails",
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = connection
            };
            command.Parameters.AddWithValue("@CustomerId", CustomerRequestDto.CustomerID);
            command.Parameters.AddWithValue("@FirstName", CustomerRequestDto.FirstName);
            command.Parameters.AddWithValue("@LastName", CustomerRequestDto.LastName);
            command.Parameters.AddWithValue("@Email", CustomerRequestDto.Email);
            command.Parameters.AddWithValue("@Mobile", CustomerRequestDto.Mobile);
            command.Parameters.AddWithValue("@RegistrationDate", CustomerRequestDto.RegistrationDate);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return Ok();
        }

        [HttpGet]
        public ActionResult GetCustomerata()
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=LAPTOP-QG4GJCJK;Database=techDb;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"
            };
            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_GetCustomerDetails",
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = connection
            };

            connection.Open();

            List<CustomerDto> response = new List<CustomerDto>();

            using (SqlDataReader sqlDataReader = command.ExecuteReader())
            {
                while (sqlDataReader.Read())
                {
                    CustomerDto customerDto = new CustomerDto();
                    customerDto.CustomerID = Convert.ToString(sqlDataReader["CustomerId"]);
                    customerDto.FirstName = Convert.ToString(sqlDataReader["FirstName"]);
                    customerDto.LastName = Convert.ToString(sqlDataReader["LastName"]);
                    customerDto.Email = Convert.ToString(sqlDataReader["Email"]);
                    customerDto.Mobile = Convert.ToString(sqlDataReader["Mobile"]);
                    customerDto.RegistrationDate = Convert.ToDateTime(sqlDataReader["RegistrationDate"]);

                    response.Add(customerDto);
                }
            }
            connection.Close();
            return Ok(JsonConvert.SerializeObject(response));
        }

        [HttpDelete]
        public ActionResult DeleteCustomerData(int customerId)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=LAPTOP-QG4GJCJK;Database=techDb;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"
            };
            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_DeleteCustomerDetails",
                CommandType = System.Data.CommandType.StoredProcedure,
                Connection = connection
            };

            connection.Open();
            command.Parameters.AddWithValue("@CustomerId", customerId);
            command.ExecuteNonQuery();
            connection.Close();
            return Ok();
        }
    }
}
