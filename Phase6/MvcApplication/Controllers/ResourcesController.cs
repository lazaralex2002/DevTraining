using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcApplication;

namespace MvcApplication.Controllers
{
    public class ResourcesController : Controller
    {
        private TaskManagementEntities db = new TaskManagementEntities();

        // GET: Resources
        public ActionResult Index()
        {
            return View(db.Resources.ToList());
        }

        // GET: Resources/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        // GET: Resources/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Resources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResourceId,ResourceName,ResourceRate")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                db.Resources.Add(resource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resource);
        }

        // GET: Resources/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        // GET: Resources/ViewTasks/5
        public ActionResult ViewTasks(int? id)
        {
            var ResourceTasks = db.ResourceTasks.Where(rt => rt.ResourceId == id).ToList();
            var TaskList = db.Tasks.ToList();
            var Tasks = new List<Task>();
            foreach (var relation in ResourceTasks)
            {
                foreach (var task in TaskList)
                {
                    if (relation.TaskId == task.TaskId)
                    {
                        Tasks.Add(task);
                    }
                }
            }
            ViewBag.Tasks = Tasks;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        // POST: Resources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResourceId,ResourceName,ResourceRate")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resource);
        }

        // GET: Resources/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        // POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resource resource = db.Resources.Find(id);
            db.Resources.Remove(resource);
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
