namespace DTO
{
    public class OrderBillDTO
    {
        public string orderbillID { get; set; }
        public string staffID { get; set; }
        public string customerID { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public double totalcost { get; set; }
        public OrderBillDTO()
        {
            totalcost = year = month = day = 0;
            customerID = staffID = orderbillID = "";
        }

    }
}
