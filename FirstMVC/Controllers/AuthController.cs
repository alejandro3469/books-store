using System.Web.Mvc;
using System.Web.Security;
using FirstMVC.Models;

namespace FirstMVC.Controllers
{
    public class AuthController : Controller
    {
        // GET
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAuth user)
        {
            if (ModelState.IsValid)
            {
                if (IsValidUser(user.Email, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    SessionManager.SetUserSession(user.Email);
                    return RedirectToAction("BooksList", "Books");
                }
                else
                {
                    ModelState.AddModelError("Email", "Invalid email or password");
                }
            }

            return View(user);
        }
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            SessionManager.ClearUserSession();
            return RedirectToAction("Login", "Auth");
        }

        private bool IsValidUser(string email, string password)
        {
            // Aquí puedes implementar la lógica para validar el usuario
            // Por ejemplo, puedes verificar las credenciales contra una base de datos
            return email == "mail@mail.com" && password == "password";
        }
    }
}