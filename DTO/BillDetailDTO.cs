namespace DTO
{
    public class BillDetailDTO
    {
        public int ID { get; set; }
        public string billID { get; set; }
        public string productID { get; set; }
        public int quantity { get; set; }
        public double totalcost { get; set; }
        public BillDetailDTO()
        {
            billID = productID = "";
            totalcost = quantity = 0;
        }
    }
}
