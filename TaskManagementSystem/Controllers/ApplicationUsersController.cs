using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApplicationUsers
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: ApplicationUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DateJoined,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicationUser);
        }

        // GET: ApplicationUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateJoined,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
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

        //Remember to provide authorization for developers alone
        [HttpGet]
        public ActionResult GetAllTaskOfADeveloper()
        {
            var userId = User.Identity.GetUserId();
            var currentUser = db.Users.Find(userId);
            var currentUserTasks = currentUser.Jobs;

            return RedirectToAction("Index", "Jobs", currentUserTasks);
        }

        [HttpGet]
        public ActionResult ManagerDashboard()
        {
            string userId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.Include(u => u.Projects).First(u => u.Id == userId);

            var projects = new List<Project>();
            projects = currentUser.Projects.Where(p => p.Priority != null).OrderByDescending(p => (int)p.Priority).ToList();
            var projectsNull = new List<Project>();
            projectsNull = currentUser.Projects.Where(p => p.Priority == null).ToList();
            projects.AddRange(projectsNull);
            ManagerDashboardViewModel viewModel = new ManagerDashboardViewModel();
            viewModel.Projects = projects;
            //currentUser.Projects.OrderByDescending(p => (int)p.Priority).ToList();
            viewModel.NumOfUnreadNotifications = NotificationHelper.NumOfUnopenedNotifications(currentUser);
            viewModel.UserId = userId;

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult DeveloperHomePage()
        {
            string userId = User.Identity.GetUserId();
            ApplicationUser user = db.Users.Find(userId);
            NotificationHelper.TasksAboutToDue(user);

            ViewBag.NumOfUnreadNotifications = NotificationHelper.NumOfUnopenedNotifications(user);
            return View(user);
        }
        [HttpGet]
        public ActionResult ShowUncopletedTasksPassedDeadline()
        {
            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser manager = db.Users.Find(userId);
            if (manager == null)
            {
                return HttpNotFound();
            }

            var result = ProjectManager.GetAllUncompletedJobsThatPassedDeadline(manager);
            return View(result);
        }

        public ActionResult DisplayProjectOverBudget()
        {
            var overBudgets = ProjectManager.GetProjectsOverBudget();
            return View(overBudgets);
        }
    }
}
