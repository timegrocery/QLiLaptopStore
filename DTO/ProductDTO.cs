namespace DTO
{
    public class ProductDTO
    {
        public string productID { get; set; }
        public string typeID { get; set; }
        public string productName { get; set; }
        public string countUnit { get; set; }
        public double cost { get; set; }
        public int warrantyTime { get; set; }
        public int quantity { get; set; }
        public string producer { get; set; }

        public ProductDTO()
        {
            productID = typeID = productName = countUnit = producer = "";
            cost = warrantyTime = quantity = 0;
        }
    }
}
