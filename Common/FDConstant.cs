using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class FDConstant
    {
        #region DBConnectionsring
        public static string TOAEDataBaseConnection = "TOEADBConnection";
        #endregion

        #region LoginSP's
        public static string SP_UserLogin = "[UserLoginWithRoleDetails]";
        public static string SP_UserLogoff = "[usp_UserLogff]"; //Added for Audit Trail
        

        #endregion

       

        public static string UserAuthorize = "[UserAuthorize]";
        public static string SP_AuthorizeTokanStore = "[AuthorizeTokanStore]";
    }
}
