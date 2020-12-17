using System.Data;
using DAO;
namespace BUS
{
    public class JobBUS
    {
        public static DataTable Load_DSCV()
        {
            return JobDAO.Load_DSCV();
        }
    }
}
