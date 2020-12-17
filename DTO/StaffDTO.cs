namespace DTO
{
    public class StaffDTO
    {
        public string staffID { get; set; }
        public string staffName { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public string gender { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string jobid { get; set; }
        /// <summary>
        /// tạo constructor
        /// mặt định rỗng
        /// </summary>
        public StaffDTO()
        {
            staffID = "";
            staffName = "";
            day = month = year = 0;
            gender = "";
            phone = "";
            address = "";
            email = "";
            jobid = "";
        }

    }
}
