﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Models;
using TaskManagementSystem.ViewModels;

namespace TaskManagementSystem.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Projects
        public ActionResult Index()
        {
            var projects = db.Projects.Include(p => p.ApplicationUser);
            return View(projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            //ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "Email");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Budget,DateCreated,ApplicationUserId")] Project project)
        {
            if (ModelState.IsValid)
            {
                var currentUser = GetCurrentUser();
                project.ApplicationUser = currentUser;
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Details",project);
            }

            //ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "Email", project.ApplicationUserId);
            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "Email", project.ApplicationUserId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Budget,DateCreated,ApplicationUserId")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.ApplicationUserId = new SelectList(db.ApplicationUsers, "Id", "Email", project.ApplicationUserId);
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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

        //Remember to provide authorization/role for project manager alone
        [HttpGet]
        public ActionResult GetAllProjectsAndTasks()
        {
            var currentUser = GetCurrentUser();
            var currentUserProjects = db.Projects.Where(p => p.ApplicationUser.Id == currentUser.Id);

            //var viewmodel = new ProjectTaskViewModel()
            //{
            //    Project = currentUserProjects.ToList(),
            //    Tasks = currentUserProjects.All
            //};
            //.OrderByDescending(p => p.Jobs.Select(j => j.CompletionPercentage));
            //return RedirectToAction("Index", "Projects", currentUserProjects);
            return View(currentUserProjects);
        }

        [HttpGet]
        public ActionResult GetTasksOrderByCompletion(int? projectId)
        {
            var result = db.Tasks.Where(t => t.ProjectId == projectId)
                .OrderByDescending(t => t.CompletionPercentage);

            return View(result);
        }

        [HttpGet]
        public ActionResult GetAllTasksHideCompletedTasks(int? projectId)
        {
            var result = db.Tasks.Where(t => t.ProjectId == projectId & !t.IsComplete)
                .OrderByDescending(t => t.CompletionPercentage);

            return View(result);
        }
        private ApplicationUser GetCurrentUser()
        {
            var a = User.Identity.GetUserId();
            var b = db.Users.Find(a);

            return b;
        }
    }
}
