namespace DTO
{
    public class AccountDTO
    {
        public int ID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string jobID { get; set; }
        public bool status { get; set; }

        public AccountDTO()
        {
            ID = 0;
            username = password = jobID = "";
            status = false;
        }
    }
}
