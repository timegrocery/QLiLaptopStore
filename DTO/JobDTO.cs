namespace DTO
{
    public class JobDTO
    {
        public string jobID { get; set; }
        public string jobName { get; set; }
        public JobDTO()
        {
            jobID = jobName = "";
        }
    }
}
