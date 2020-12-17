namespace DTO
{
    public class ProductTypeDTO
    {
        public string typeID { get; set; }
        public string productTypeName { get; set; }
        public ProductTypeDTO()
        { typeID = productTypeName = ""; }
    }
}
