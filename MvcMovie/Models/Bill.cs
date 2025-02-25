namespace MvcMovie.Models
{
    public class Bill
    {
        public string ProductName { get; set; } = string.Empty; // ✅
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } // ✅ 

        // Tính tổng tiền
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}