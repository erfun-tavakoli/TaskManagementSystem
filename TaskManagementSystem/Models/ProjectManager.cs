using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TaskManagementSystem.Models
{
    public class ProjectManager
    {
        static ApplicationDbContext db = new ApplicationDbContext();

        public static void AddProject(string projectName, double budget, DateTime dateCreated)
        {
            var project = db.Projects.Any(x => x.Title == projectName);

            if (!project)
            {
                var e = new Project();
                e.Title = projectName;
                e.Budget = budget;
                e.DateCreated = dateCreated;
                db.Projects.Add(e);
                db.SaveChanges();
            }
        }

        public static void DeleteProject(int id)
        {
            var project = db.Projects.Any(p => p.Id == id);

            if (project)
            {
                var pp = db.Projects.Find(id);
                db.Projects.Remove(pp);
                db.SaveChanges();
            }
        }

        //public static List<Project> GetAllProjectsForProjectManager(string managerId)
        //{
        //    ApplicationUser manager = db.Users.Find(managerId);

        //    if (manager != null)
        //    {
        //        return manager.Projects.ToList();
        //    }


        //}
    }
}