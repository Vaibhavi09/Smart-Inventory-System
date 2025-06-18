namespace TechApi.Models
{
    public class InventoryDto
    {
        #region Properties
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public int StockAvailable { get; set; }
        public int ReorderStock { get; set; }
        #endregion
    }
}
