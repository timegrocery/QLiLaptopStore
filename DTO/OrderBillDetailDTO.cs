namespace DTO
{
    public class OrderBillDetailDTO
    {
        public int ID { get; set; }
        public string orderbillID { get; set; }
        public string productID { get; set; }
        public int quantity { get; set; }
        public double totalcost { get; set; }
        public OrderBillDetailDTO()
        {
            totalcost = ID = quantity = 0;
            orderbillID = productID = "";
        }
    }
}
