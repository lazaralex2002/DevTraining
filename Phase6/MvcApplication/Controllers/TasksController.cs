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
    public class TasksController : Controller
    {
        private TaskManagementEntities db = new TaskManagementEntities();
        // GET: Tasks
        /*
        public ActionResult Index()
        {
            if (HttpContext.Session["Project"] == null)
            {
                return Redirect("/Projects/ChooseProject");
            }
            else
            {
                ViewBag.TaskCost = db.GetTaskCost().ToList();
                var proj = int.Parse(HttpContext.Session["Project"].ToString());
                var tasks = db.Tasks.Include(t => t.Project).Where(t => t.ProjectId == proj);
                return View(tasks.ToList());
            }
        }
        */

        public ActionResult Index(string project)
        {
            ViewBag.TaskCost = db.GetTaskCost().ToList();
            ViewBag.Projects = db.Projects.ToList();
            if (HttpContext.Session["Project"] != null)
            {
                var proj = int.Parse(HttpContext.Session["Project"].ToString());
                Redirect("/Tasks/?project=" + proj.ToString());
            }
            project = Request.QueryString["project"];
            if (project == null || project == "")
            {
                var tasks = db.Tasks.Include(t => t.Project);
                return View(tasks.ToList());
            }
            else
            {
                ViewBag.TaskCost = db.GetTaskCost().ToList();
                var proj = int.Parse(project);
                var tasks = db.Tasks.Include(t => t.Project).Where(t => t.ProjectId == proj);
                return View(tasks.ToList());
            }
        }
        // GET: Tasks/AssignResource/5
        public ActionResult AssignResource(int? id)
        {
    
            var TaskResources = db.ResourceTasks.Where(rt => rt.TaskId == id).ToList();
            var ResourceList = db.Resources.ToList();
            var ResourcesThatCanBeAssigned = new List<Resource>();
            var AssignedResources = new List<Resource>();
            foreach (var resource in ResourceList)
            {
                bool ok = true;
                foreach(var taskResource in TaskResources)
                {
                    if (resource.ResourceId == taskResource.ResourceId) ok = false;
                }
                if(ok == true)
                {
                    ResourcesThatCanBeAssigned.Add(resource);
                }
                else
                {
                    AssignedResources.Add(resource);
                }
            }
            ViewBag.AssignedResources = AssignedResources;
            ViewBag.ResourcesThatCanBeAssigned = ResourcesThatCanBeAssigned;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/ViewResources/5
        public ActionResult ViewResources(int? id)
        {
            var TaskResources = db.ResourceTasks.Where(rt => rt.TaskId == id).ToList();
            var ResourceList = new List<Resource>();
            foreach (var taskResource in TaskResources)
            {
                var result = db.Resources.Where(res => res.ResourceId == taskResource.ResourceId);
                if (result != null)
                {
                    ResourceList.Add(result.First());
                }
            }
            ViewBag.Resources = ResourceList;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskId,Name,Duration,Start,Finish,TaskMode,ProjectId")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", task.ProjectId);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", task.ProjectId);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskId,Name,Duration,Start,Finish,TaskMode,ProjectId")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", task.ProjectId);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
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
