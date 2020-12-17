using DTO;
using DAO;
using System.Data;
namespace BUS
{
    public class StaffBUS
    {
        public static DataTable loadStaffList()
        {
            return StaffDAO.loadStaffList();
        }
        public static void insertStaff(StaffDTO staff)
        {
            StaffDAO.insertStaff(staff);
        }
        public static void updateStaff(StaffDTO staff)
        {
            StaffDAO.updateStaff(staff);
        }
        public static void deleteStaff(StaffDTO staff)
        {
            StaffDAO.deleteStaff(staff);
        }
        public static DataTable searchStaffByName(StaffDTO staff)
        {
            return StaffDAO.searchStaffByName(staff);
        }
        public static DataTable searchStaffByID(StaffDTO staff)
        {
            return StaffDAO.searchStaffByID(staff);
        }
        public static DataTable searchStaffByJob(JobDTO job)
        {
            return StaffDAO.searchStaffByJob(job);
        }
    }
}
