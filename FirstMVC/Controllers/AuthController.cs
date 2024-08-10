using System;
using System.Collections.Generic;
using System.Web;
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

            var peticion = Request;
            if (ModelState.IsValid)
            {
                if (IsValidUser(user.Email, user.Password))
                {
                    // FormsAuthentication.SetAuthCookie(user.Email, false);
                    // SessionManager.SetUserSession(user.Email);
                    // return RedirectToAction("BooksList", "Books");

                    var claims = new List<string> { user.Email, "10", "Admin" }; // Replace with actual claims

                    // Create the authentication ticket
                    var authTicket = new FormsAuthenticationTicket(
                        1, // version
                        user.Email, // user name
                        DateTime.Now, // issue time
                        DateTime.Now.AddMinutes(30), // expiration time (TTL)
                        false, // persistent
                        string.Join(",", claims) // user data (claims)
                    );

                    // Encrypt the ticket
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                    // Create the cookie
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                    {
                        Expires = authTicket.Expiration
                    };

                    // Add the cookie to the response
                    Response.Cookies.Add(authCookie);

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