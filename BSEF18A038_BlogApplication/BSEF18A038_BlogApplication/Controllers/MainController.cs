using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BSEF18A038_BlogApplication.Models;

namespace BSEF18A038_BlogApplication.Controllers
{
    public class MainController : Controller
    {
        private readonly IWebHostEnvironment Environment;
        public MainController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }

        public ViewResult Index()
        {
            PostRepository.posts = DBHandler.GetPosts();
            return View("~/Views/Main/MainView.cshtml", PostRepository.posts);
        }


        [HttpGet]
        public ViewResult CreateNewPost()
        {
            return View("~/Views/Main/CreatePostView.cshtml");
        }


        [HttpPost]
        public ViewResult CreateNewPost(Post p)
        {
            if (ModelState.IsValid)
            {
                p.UserId = (int)HttpContext.Session.GetInt32("UserId");
                p.UserEmail = HttpContext.Session.GetString("UserEmail");
                p.UserProfilePicture= HttpContext.Session.GetString("UserProfilePicture");
                if (DBHandler.addPost(p))
                {
                    return View("~/Views/Main/PostView.cshtml", p);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Post Creation Failed...");
                    return View("~/Views/Main/CreatePostView.cshtml");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please Enter Correct Data");
                return View("~/Views/Main/CreatePostView.cshtml");
            }

        }


        [HttpGet]
        public ViewResult EditPost(int id)
        {
            Post p = PostRepository.posts.Find(s => s.Id == id);
            return View("~/Views/Main/EditPostView.cshtml", p);
        }


        [HttpPost]
        public ViewResult EditPost(Post ps)
        {
            if (ModelState.IsValid)
            {
                if (DBHandler.UpdatePost(ps))
                {
                    PostRepository.posts = DBHandler.GetPosts();
                    Post p = PostRepository.posts.Find(s => s.Id == ps.Id);
                    return View("~/Views/Main/PostView.cshtml", p);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Post Updating Failed...");
                    return View("~/Views/Main/EditPostView.cshtml", ps);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Enter Correct Data...");
                return View("~/Views/Main/EditPostView.cshtml", ps);
            }
        }


        public ViewResult PostView(int id)
        {
            Post p = PostRepository.posts.Find(s => s.Id == id);
            return View("~/Views/Main/PostView.cshtml", p);
        }


        public ViewResult DeletePost(int id)
        {
            if (DBHandler.DeletePost(id))
            {
                PostRepository.posts = DBHandler.GetPosts();
                return View("~/Views/Main/MainView.cshtml", PostRepository.posts);
            }
            else
            {
                Post p = PostRepository.posts.Find(s => s.Id == id);
                ModelState.AddModelError(string.Empty, "Post Updating Failed...");
                return View("~/Views/Main/EditPostView.cshtml", p);
            }
        }


        public ViewResult Logout()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                HttpContext.Session.Remove("UserId");
                HttpContext.Session.Remove("UserName");
                HttpContext.Session.Remove("UserEmail");
                HttpContext.Session.Remove("UserPassword");
                HttpContext.Session.Remove("UserProfilePicture");
            }
            return View("~/Views/Home/LoginForm.cshtml");
        }


        [HttpGet]
        public ViewResult UpdateProfile()
        {

            User u = DBHandler.GetUser(HttpContext.Session.GetString("UserEmail"));
            return View("~/Views/Main/ProfileView.cshtml", u);
        }


        [HttpPost]
        public ViewResult UpdateProfile(User u, IFormFile profile_image, string newPassword)
        {
            if (ModelState.IsValid && newPassword!=null && profile_image!=null)
            {
                var obj = DBHandler.validUser(u.Email, u.Password);
                User us = obj.Item2;
                if (obj.Item1)
                {
                    string wwwPath = this.Environment.WebRootPath;
                    string contentPath = this.Environment.ContentRootPath;

                    string path = Path.Combine(this.Environment.WebRootPath, "Images");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string uploadedFiles = string.Empty;
                    string fileName = Path.GetFileName(profile_image.FileName);
                    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        profile_image.CopyTo(stream);
                        uploadedFiles = fileName;
                    }
                    if (DBHandler.UpdateUser(u, newPassword , fileName))
                    {
                        us = DBHandler.GetUser(u.Email);
                       
                        return View("~/Views/Main/ProfileView.cshtml", us);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Post Updating Failed...");
                        return View("~/Views/Main/ProfileView.cshtml", u);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Enter Correct Old Password");
                    return View("~/Views/Main/ProfileView.cshtml", u);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Enter Correct Data...");
                u = DBHandler.GetUser(u.Email);
                return View("~/Views/Main/ProfileView.cshtml", u);
            }
        }
    }

}
