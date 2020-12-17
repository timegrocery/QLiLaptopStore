namespace DTO
{
    public class BillDTO
    {
        public string billID { get; set; }
        public string staffID { get; set; }
        public string customerID { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public double paid { get; set; }
        public double left { get; set; }
        public bool pay { get; set; }
        public double totalcost { get; set; }

        public BillDTO()
        {
            billID = customerID = staffID = "";
            year = month = day = 0;
            pay = false;
            totalcost = paid = left = 0;
        }
    }
}
