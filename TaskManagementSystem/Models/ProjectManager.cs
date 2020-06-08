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

        public static List<Project> GetProjectsOverBudget()
        {
            var projectsOverBudget = new List<Project>();
            foreach (var project in db.Projects)
            {
                var projectCost = GetProjectCost(project.Id);
                if (projectCost > project.Budget )
                {
                    projectsOverBudget.Add(project);

                }
            }
            return projectsOverBudget;
        }

        public static double GetProjectCost(int? Id)
        {
            var currentProject = db.Projects.Find(Id);
            
            var timeNow = DateTime.Now;
            TimeSpan taskDuration;
            double totalProjectCost = 0;
            double totalCostPerDeveloper;
            double developerRate;

            foreach (var task in currentProject.Jobs)
            {
                foreach (var developer in task.Developers)
                {
                    developerRate = developer.DailyRate;
                    taskDuration = timeNow - task.DateCreated;
                    totalCostPerDeveloper = taskDuration.TotalDays * developerRate;
                    totalProjectCost += totalCostPerDeveloper;
                }
            }
            return totalProjectCost;
        }
     

        public static List<Job> GetAllUncompletedJobsThatPassedDeadline(ApplicationUser manager)
        {
            return manager.Projects.SelectMany(p => p.Jobs).Where(j => !j.IsComplete && TaskManager.CheckIfTaskPassedDeadline(j)).ToList();
        }

        public static bool ProjectHasPassedDeadlineWithUnfinishedJobs(Project project)
        {
            if (project.Deadline != null)
            {
                int compaireResult = DateTime.Compare((DateTime)project.Deadline, DateTime.Now);

                if (compaireResult < 0)
                    return false;

                return !project.Jobs.All(j => j.IsComplete);
            }
            return false;
        }

        public static int GetNumOfUnfinishedJobsForProject(Project project)
        {
            return project.Jobs.Count(j => !j.IsComplete);
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