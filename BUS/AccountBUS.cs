using DTO;
using DAO;
namespace BUS
{
    public class AccountBUS
    {
        public static void register(AccountDTO tk)
        {
            AccountDAO.insertAccount(tk);
        }
        public static string login(AccountDTO tk)
        {
            return AccountDAO.login(tk);
        }
        public static void logout(AccountDTO tk)
        {
            AccountDAO.logout(tk);
        }
    }
}
