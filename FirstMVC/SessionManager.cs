using System.Web;

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
    }
}