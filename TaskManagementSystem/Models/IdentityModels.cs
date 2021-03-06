﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TaskManagementSystem.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public double DailyRate { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public DateTime DateJoined { get; set; }

        public ApplicationUser()
        {
            this.Projects = new HashSet<Project>();
            this.Jobs = new HashSet<Job>();
            this.Notifications = new HashSet<Notification>();
            DateJoined = DateTime.Now;
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Project>Projects { get; set; }
        public DbSet<Job>Tasks { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public ApplicationDbContext()
            : base("ProjectManagementDBConnectionString", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}

















