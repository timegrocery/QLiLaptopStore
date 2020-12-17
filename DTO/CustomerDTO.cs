namespace DTO
{
    public class CustomerDTO
    {
        public string customerID { get; set; }
        public string customerName { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public CustomerDTO()
        {
            customerID = customerName = address = phoneNumber = "";
        }
    }
}
