namespace DTO
{
    public class SupplierDTO
    {
        public string supplierID { get; set; }
        public string supplierName { get; set; }
        public string supplierPhone { get; set; }
        public string email { get; set; }

        public SupplierDTO()
        {
            supplierID = supplierName = supplierPhone = email = "";
        }
    }
}
