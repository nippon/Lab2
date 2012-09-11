using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab2.Models.Repositories;
using Lab2.Models.Entities;
using Lab2.ViewModels;

namespace Lab2.Controllers
{
    public class ForumsController : Controller
    {
        //
        // GET: /Forums/

        public ActionResult Index()
        {
            List<ForumThread> threads = Repository.Instance.GetSortedThreads();
            
            return View(threads);
        }

        public ActionResult Thread(Guid id)
        {
            ThreadViewModel tvm = new ThreadViewModel();
            tvm.ForumThread = Repository.Instance.Get<ForumThread>(id);
            tvm.Post = Repository.Instance.GetPostsByThreadID(id);
            return View(tvm);
        }
        
        public ActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost(Post post)
        {
            if ( ModelState.IsValid )
            {
                Repository.Instance.Save<Post>(post);
                ForumThread forumthread = new ForumThread();
                Repository.Instance.Save<ForumThread>( forumthread );
                post.ThreadID = forumthread.ID;
                forumthread.Title = post.Title;
                forumthread.CreateDate = post.CreateDate;
                post.CreatedByID = Repository.Instance.GetUserByUserName( User.Identity.Name ).ID;

                return RedirectToAction("Thread", "Forums", new { id = forumthread.ID });
            }
            string msg = "Invalid input."; //Fixa ett felmeddelande
            return View(msg);
        }
    }
}
