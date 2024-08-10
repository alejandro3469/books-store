using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace FirstMVC
{
    public static class SessionManager
    {
        private const string UserSessionKey = "UserSession";

        public static void SetUserSession(string email)
        {
            HttpContext.Current.Session[UserSessionKey] = email;
        }

        public static string GetUserSession()
        {
            return HttpContext.Current.Session[UserSessionKey] as string;
        }

        public static void ClearUserSession()
        {
            HttpContext.Current.Session.Remove(UserSessionKey);
        }
        
        public static List<string> GetUserClaims()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated) return new List<string>();
            
            var identity = (FormsIdentity)HttpContext.Current.User.Identity;
            var ticket = identity.Ticket;
            var userData = ticket.UserData;
            return userData.Split(',').ToList();
        }
    }
}