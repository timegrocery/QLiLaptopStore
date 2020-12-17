namespace DTO
{
    public class WarrantyDetailDTO
    {
        public int ID { get; set; }
        public string warrantyID { get; set; }
        public string productID { get; set; }
        public string detail { get; set; }

        public WarrantyDetailDTO()
        {
            ID = 0;
            warrantyID = productID = detail = "";
        }
    }
}
