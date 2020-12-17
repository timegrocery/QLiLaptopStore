namespace DTO
{
    public class ImportBillDTO
    {
        public int ID { get; set; }
        public string ImportBillID { get; set; }
        public string ProductID { get; set; }
        public int quantity { get; set; }
        public double cost { get; set; }
        public double total { get; set; }
        public ImportBillDTO()
        {
            ImportBillID = ProductID = "";
            cost = total = ID = quantity = 0;
        }
    }
}
