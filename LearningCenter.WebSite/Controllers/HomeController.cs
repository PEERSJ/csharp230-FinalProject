using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearningCenter.Business;
using LearningCenter.WebSite.Models;
using System.Runtime.InteropServices;

//Jeff Peerson  -  Csharp230 - Final Project - Learning Center website  -  May 8, 2017

namespace LearningCenter.WebSite.Controllers
{

    public class HomeController : Controller
    {

        //HomeController constructor 
        private readonly IClassManager classManager;
        private readonly IUserManager userManager;
  

        public HomeController(IClassManager classManager,         
                                IUserManager userManager)
        {
            this.classManager = classManager;
            this.userManager = userManager;
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel registerViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = this.userManager.Register(registerViewModel.Email, registerViewModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User Password and confirm password do not match.");
                }
                else
                {
                    Session["User"] = new LearningCenter.WebSite.Models.UserModel { Id = user.Id, Name = user.Name, Classes = user.Classes?.Select(c => new LearningCenter.WebSite.Models.ClassModel(c.Id, c.Name, c.Description, c.Price)).ToArray()};

                    System.Web.Security.FormsAuthentication.SetAuthCookie(registerViewModel.Email, false);

                    return Redirect(returnUrl ?? "~/");
                }
            }

            return View(registerViewModel);
        }


        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = this.userManager.LogIn(loginModel.UserName, loginModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    //Session["User"] = new LearningCenter.WebSite.Models.UserModel { Id = user.Id, Name = user.Name };
                    Session["User"] = new LearningCenter.WebSite.Models.UserModel { Id = user.Id, Name = user.Name, Classes = user.Classes?.Select(c => new LearningCenter.WebSite.Models.ClassModel(c.Id, c.Name, c.Description, c.Price)).ToArray() };

                    System.Web.Security.FormsAuthentication.SetAuthCookie(loginModel.UserName, false);

                    return Redirect(returnUrl ?? "~/");
                }
            }

            return View(loginModel);
        }

        public ActionResult LogOff()
        {
            Session["User"] = null;
            System.Web.Security.FormsAuthentication.SignOut();

            return Redirect("~/");
        }




        public ActionResult Index()
        {

            var classes = this.classManager.Classes
                                            .Select(t => new LearningCenter.WebSite.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                                            .ToArray();
            var model = new IndexModel { Classes = classes };
            return View(model);
        }


        public ActionResult Class()
        {
            var classes = this.classManager.Classes
                                            .Select(t => new LearningCenter.WebSite.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                                            .ToArray();
            var model = new ClassViewModel { Classes = classes };
            return View(model);
        }


        [Authorize]
        public ActionResult StudentClasses()
        {
            var user = Session["User"] as LearningCenter.WebSite.Models.UserModel;
            var model = new StudentClassesViewModel { Classes = user.Classes };
            return View(model);

        }


        [Authorize]
        public ActionResult AvailableClasses()
        {
            var user = Session["User"] as LearningCenter.WebSite.Models.UserModel;
            //check that user != null
            var classes = this.classManager.Classes.Where(c => !user.Classes.Any(uc => uc.Id == c.Id))
                                .Select(t => new LearningCenter.WebSite.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                                .ToArray();

            var model = new EnrollInClassViewModel { Classes = classes, User = user };
            return View(model);

        }



        //Enroll action which requires the user to be logged in (Authorize).
        [Authorize]
        public ActionResult EnrollInClass(int classId)
        {
            var user = Session["User"] as LearningCenter.WebSite.Models.UserModel;

            if (classId > 0)
            {
                this.userManager.Enroll(user.Id, classId);

                List<Models.ClassModel> classes = user.Classes?.ToList() ?? new List<Models.ClassModel>();
                var cl = classManager.Class(classId);
                classes.Add(new Models.ClassModel(classId, cl.Name, cl.Description, cl.Price));
                user.Classes = classes.ToArray();
                Session["User"] = user;

                var classeses = this.classManager.Classes.Where(c => !user.Classes.Any(uc => uc.Id == c.Id))
                                    .Select(t => new LearningCenter.WebSite.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                                    .ToArray();

                var model = new EnrollInClassViewModel { Classes = classeses, User = user };
                return View(model);
            }
            else
            {
                //check that user != null
                var classes = this.classManager.Classes.Where(c => !user.Classes.Any(uc => uc.Id == c.Id))
                                    .Select(t => new LearningCenter.WebSite.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                                    .ToArray();

                var model = new EnrollInClassViewModel { Classes = classes, User = user };
                return View(model);
            }

        }





        public ActionResult About()
        {
            ViewBag.Message = "Jeff Peerson  -  CSharp230 - Final Project - 'Learning Center' Website  -  May 8, 2017";

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Jeff Peerson  /  GO DAWGS!";

            return View();
        }
    }
}