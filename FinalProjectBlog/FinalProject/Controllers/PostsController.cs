using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject;

namespace FinalProject.Controllers
{
   
    public class PostsController : Controller
    {
        private ThoughtBoxEntities db = new ThoughtBoxEntities();

        // GET: Posts
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.myUser);
            return View(posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            ViewBag.userId = new SelectList(db.myUsers, "userId", "Username");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post, HttpPostedFileBase file)
        {
              try
            {
                var supportedTypes = new[] { "jpg", "png", "gif", "tif" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                var uname = User.Identity.Name;

               
                if (ModelState.IsValid)
                {
                    if (!supportedTypes.Contains(fileExt))
                    {
                        ModelState.AddModelError("ImagePath", "File Extension Is InValid - Only Upload JPEG/PNG/GIF/TIFF File");
                    }
                    else if (file.ContentLength > (1 * 1024))
                    {
                        ModelState.AddModelError("ImagePath", "File size is too big");
                    }
                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);

                        db.Posts.Add(new Post
                        {   
                            postId = post.postId,
                            Title = post.Title,
                            Category = post.Category,
                            Post1 = post.Post1,
                            Date = DateTime.Now,
                            postedBy = User.Identity.Name,
                           
                            //PostedBy = uame.ToString(),
                            ImagePath = "~/Images/" + file.FileName
                        }) ;
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ImagePath", "Please upload file. It will make your blog nice");
            }

            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
{
    if (id == null)
    {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    }
    Post post = db.Posts.Find(id);
    if (post == null)
    {
        return HttpNotFound();
    }
    ViewBag.userId = new SelectList(db.myUsers, "userId", "Username", post.userId);
    return View(post);
}

// POST: Posts/Edit/5
// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Edit([Bind(Include = "postId,userId,Title,Category,Post1,Date,ImagePath,postedBy")] Post post)
{
    if (ModelState.IsValid)
    {
        db.Entry(post).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
    }
    ViewBag.userId = new SelectList(db.myUsers, "userId", "Username", post.userId);
    return View(post);
}

// GET: Posts/Delete/5
public ActionResult Delete(int? id)
{
    if (id == null)
    {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    }
    Post post = db.Posts.Find(id);
    if (post == null)
    {
        return HttpNotFound();
    }
    return View(post);
}

// POST: Posts/Delete/5
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public ActionResult DeleteConfirmed(int id)
{
    Post post = db.Posts.Find(id);
    db.Posts.Remove(post);
    db.SaveChanges();
    return RedirectToAction("Index");
}

protected override void Dispose(bool disposing)
{
    if (disposing)
    {
        db.Dispose();
    }
    base.Dispose(disposing);
}
    }
}
