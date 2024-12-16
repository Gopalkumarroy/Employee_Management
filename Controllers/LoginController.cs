using Employee_Management_Project.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Employee_Management_Project.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();   
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model)
        {
            

            using (EmpDbEntities db = new EmpDbEntities())
            {
                // Hash the entered password before comparison
              //  string hashedPassword = HashPassword(model.Password);

                var user = db.Employees
                             .FirstOrDefault(u => u.Emp_Email == model.Emp_Email && u.Password == model.Password);

                if (user != null)
                {
                    // Authentication logic
                    Session["Username"] = user.Emp_Name;  // Store user in session
                    return RedirectToAction("ViewPage", "Employee");
                }

                ModelState.AddModelError("", "Invalid email or password. Please try again.");
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            // Clear session and cookies on logout
            Session.Clear();
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));

            return RedirectToAction("Login", "Login");
        }

        // Utility method to hash the password using SHA256
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (var t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }

    // Custom Action Filter for session authorization
    public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Access session via HttpContext
            if (HttpContext.Current.Session["Username"] == null)
            {
                // Redirect to Login if session is null
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                    {
                        { "controller", "Login" },
                        { "action", "Login" }
                    });
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
