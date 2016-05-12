using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTema2PS.Models;
using WebMatrix.WebData;
using System.Security.Cryptography;
using System.Text;

namespace MVCTema2PS.Controllers
{
    public class UserController : Controller
    {
        private UserDBContext db = new UserDBContext();
        private ShowDBContext showDB = new ShowDBContext();

        //
        // GET: /User/

        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        //
        // GET: /User/Details/5


        public ActionResult Details(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (Session["UserRole"].Equals("Administrator"))
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //
        // GET: /User/Create

   
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            user.usertype = "Employee";
            user.password = getMd5Hash(user.password);
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(user);
           
        }

        //
        // GET: /User/Edit/5

   
        public ActionResult Edit(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            
            if (Session["UserRole"].Equals("Administrator"))
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        
        
        
        }




        //
        // POST: /User/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /User/Delete/5
    
        public ActionResult Delete(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (Session["UserRole"].Equals("Administrator"))
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User u)
        {
            //this action is for handle post (login)
            if (ModelState.IsValid)//this checks validity
            {
                using (UserDBContext dc = new UserDBContext())
                {
                    String pass = UserController.getMd5Hash(u.password);
                    var v = dc.Users.Where(a => a.username.Equals(u.username) && a.password.Equals(pass)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserID"] = v.ID.ToString();
                        Session["LoggedUserFirstname"] = v.firstname.ToString();
                        Session["LoggedUserLastname"] = v.lastname.ToString();
                        Session["UserRole"] = v.usertype.ToString();
                        if (v.usertype.Equals("Administrator"))
                        {
                            return RedirectToAction("AdminAfterLogin");
                        }
                        else
                        {
                            return RedirectToAction("EmployeeAfterLogin");
                        }
                    }
                }
            }
            return View(u);
        }


        public ActionResult Logoff()
        {
            Session["LogedUserID"] = null;
            Session["LoggedUserFirstname"] = null;
            Session["LoggedUserLastname"] = null;
            Session["UserRole"] = null;
            return RedirectToAction("Login", "User");
        }



        public ActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(User user)
        {

            using (UserDBContext dc = new UserDBContext())
            {
                User v = dc.Users.Where(a => a.username.Equals(user.username)).FirstOrDefault();
                if (v != null)
                {
                    String newPassword = RandomPasswordGenerator.Generate();
                    ViewBag.NewPassword = newPassword;
                    v.password = getMd5Hash(newPassword);
                    dc.Entry(v).State = EntityState.Modified;
                    dc.SaveChanges();
                }
                else
                {
                    ViewBag.UnknownUser = "Unknown User";
                }
            }

            return View(user);
        }





        public ActionResult AdminAfterLogin()
        {
            if (Session["UserRole"].Equals("Administrator"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

      
        public ActionResult EmployeeAfterLogin()
        {
            if (Session["UserRole"].Equals("Employee"))
            {
                return View(showDB.Shows.ToList());
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}