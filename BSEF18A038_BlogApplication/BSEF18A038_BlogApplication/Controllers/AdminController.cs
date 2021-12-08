using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSEF18A038_BlogApplication.Models;

namespace BSEF18A038_BlogApplication.Controllers
{
    public class AdminController : Controller
    {
        public ViewResult Index()
        {
            if (HttpContext.Session.GetString("AdminEmail") != null)
            {
                UserRepository.users = DBHandler.getUsersList();
                return View("~/Views/Admin/AdminMainView.cshtml", UserRepository.users);
            }
            return View("~/Views/Home/AdminLoginForm.cshtml");
        }
        public ViewResult Logout()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                HttpContext.Session.Remove("AdminEmail");
                HttpContext.Session.Remove("AdminPassword");
            }
            return View("~/Views/Home/AdminLoginForm.cshtml");
        }
        public ViewResult DeleteUser(int id)
        {
            if (HttpContext.Session.GetString("AdminEmail") != null)
            {
                if (DBHandler.DeleteUser(id))
                {
                    UserRepository.users = DBHandler.getUsersList();
                    return View("~/Views/Admin/AdminMainView.cshtml", UserRepository.users);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Post Updating Failed...");
                    return View("~/Views/Admin/AdminMainView.cshtml", UserRepository.users);
                }
            }
            return View("~/Views/Home/AdminLoginForm.cshtml");
        }
        [HttpGet]
        public ViewResult UpdateUser(int id)
        {
            if (HttpContext.Session.GetString("AdminEmail") != null)
            {
                User us = UserRepository.users.Find(u => u.Id == id);
                return View("~/Views/Admin/UpdateUserView.cshtml", us);
            }
            return View("~/Views/Home/AdminLoginForm.cshtml");
        }
        [HttpPost]
        public ViewResult UpdateUser(User u)
        {
            if (HttpContext.Session.GetString("AdminEmail") != null)
            {
                if (ModelState.IsValid)
                {
                    if(DBHandler.UpdateUserByAdmin(u))
                    {
                        UserRepository.users = DBHandler.getUsersList();
                        ModelState.AddModelError(string.Empty, "Account Updated successfully...");
                        User us = UserRepository.users.Find(use => use.Id == u.Id);
                        return View("~/Views/Admin/UpdateUserView.cshtml", us);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "User Updation Failed...");
                        User us = UserRepository.users.Find(use => use.Id == u.Id);
                        return View("~/Views/Admin/UpdateUserView.cshtml", us);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Enter Data in all Field...");
                    User us = UserRepository.users.Find(use => use.Id == u.Id);
                    return View("~/Views/Admin/UpdateUserView.cshtml", us);
                }
            }
            return View("~/Views/Home/AdminLoginForm.cshtml");
        }

        [HttpGet]
        public ViewResult CreateUser(int id)
        {
            if (HttpContext.Session.GetString("AdminEmail") != null)
            {
                return View("~/Views/Admin/CreateUserForm.cshtml");
            }
            return View("~/Views/Home/AdminLoginForm.cshtml");
        }

        [HttpPost]
        public ViewResult CreateUser(User u)
        {
            if (HttpContext.Session.GetString("AdminEmail") != null)
            {
                if (ModelState.IsValid)
                {
                    if (DBHandler.registerUser(u))
                    {
                        UserRepository.users = DBHandler.getUsersList();
                        return View("~/Views/Admin/AdminMainView.cshtml", UserRepository.users);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Registeration Failed...");
                        return View("~/Views/Admin/CreateUserForm.cshtml");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please Enter Data in all Fields...");
                    return View("~/Views/Admin/CreateUserForm.cshtml");
                }
            }
            else
            {
                return View("~/Views/Home/AdminLoginForm.cshtml");
            }
        }
    }
}
