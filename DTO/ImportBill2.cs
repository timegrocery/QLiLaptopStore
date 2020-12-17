namespace DTO
{
    public class ImportBill2
    {
        public string importbillID { get; set; }
        public string staffID { get; set; }
        public string supplierID { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public double totalcost { get; set; }

        public ImportBill2()
        {
            importbillID = staffID = supplierID = "";
            totalcost = year = month = day = 0;
        }
    }
}
