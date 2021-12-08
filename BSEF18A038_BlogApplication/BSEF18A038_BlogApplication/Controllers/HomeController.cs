using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BSEF18A038_BlogApplication.Models;

namespace BSEF18A038_BlogApplication.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            if(HttpContext.Session.GetString("Username")!=null)
            {
                PostRepository.posts = DBHandler.GetPosts();
                return View("~/Views/Main/MainView.cshtml", PostRepository.posts);
            }
            return View("~/Views/Home/LoginForm.cshtml");
        }


        public ViewResult About()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                PostRepository.posts = DBHandler.GetPosts();
                return View("~/Views/Main/MainView.cshtml", PostRepository.posts);
            }
            else
                return View("~/Views/Home/About.cshtml");
        }
        

        [HttpPost]
        public ViewResult Login(string email, string password)
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                PostRepository.posts = DBHandler.GetPosts();
                return View("~/Views/Main/MainView.cshtml", PostRepository.posts);
            }
            else
            {
                if (string.IsNullOrEmpty(email) == false && string.IsNullOrEmpty(password) == false)
                {

                    var obj = DBHandler.validUser(email, password);
                    User u = obj.Item2;
                    if (obj.Item1)
                    {
                        HttpContext.Session.SetInt32("UserId", u.Id);
                        HttpContext.Session.SetString("UserName", u.Username);
                        HttpContext.Session.SetString("UserEmail", u.Email);
                        HttpContext.Session.SetString("UserPassword", u.Password);
                        HttpContext.Session.SetString("UserProfilePicture", u.pictureFileName);
                        PostRepository.posts = DBHandler.GetPosts();
                        return View("~/Views/Main/MainView.cshtml", PostRepository.posts);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Account Don't Exist...");
                        return View("~/Views/Home/LoginForm.cshtml");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please Enter Correct Data");
                    return View("~/Views/Home/LoginForm.cshtml");
                }
            }
        }


        [HttpGet]
        public ViewResult SignupForm()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                PostRepository.posts = DBHandler.GetPosts();
                return View("~/Views/Main/MainView.cshtml", PostRepository.posts);
            }
            else
                return View("~/Views/Home/SiqnupForm.cshtml");
        }


        [HttpPost]
        public ViewResult SignupForm(User u)
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                PostRepository.posts = DBHandler.GetPosts();
                return View("~/Views/Main/MainView.cshtml", PostRepository.posts);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (DBHandler.registerUser(u))
                    {
                        //PostRepository.posts = DBHandler.GetPosts();
                        //return View("~/Views/Main/MainView.cshtml", PostRepository.posts);
                        ModelState.AddModelError(string.Empty, "Registred Successfully...");
                        return View("~/Views/Home/LoginForm.cshtml");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Registeration Failed...");
                        return View("~/Views/Home/SiqnupForm.cshtml");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please Enter Correct Data");
                    return View("~/Views/Home/SiqnupForm.cshtml");
                }
            }   
        }


        [HttpGet]
        public ViewResult AdminLoginForm()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                PostRepository.posts = DBHandler.GetPosts();
                return View("~/Views/Main/MainView.cshtml", PostRepository.posts);
            }
            else
                return View("~/Views/Home/AdminLoginForm.cshtml");
        }

        [HttpPost]
        public ViewResult AdminLoginForm(string adminLoginEmail , string adminLoginPassword)
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                PostRepository.posts = DBHandler.GetPosts();
                return View("~/Views/Main/MainView.cshtml", PostRepository.posts);
            }
            else
            {
                if (string.IsNullOrEmpty(adminLoginEmail) == false && string.IsNullOrEmpty(adminLoginPassword) == false)
                {
                    if (adminLoginEmail=="admin@gmail.com" && adminLoginPassword=="admin")
                    {
                        HttpContext.Session.SetString("AdminEmail", adminLoginEmail);
                        HttpContext.Session.SetString("AdminPassword", adminLoginPassword);
                        UserRepository.users = DBHandler.getUsersList();
                        return View("~/Views/Admin/AdminMainView.cshtml", UserRepository.users);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Wrong Credentials...");
                        return View("~/Views/Home/AdminLoginForm.cshtml");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Please Enter All Fields");
                    return View("~/Views/Home/AdminLoginForm.cshtml");
                }
            }
        }
    }
}
