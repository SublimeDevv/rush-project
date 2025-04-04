﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rush.Domain.Entities;
using Rush.Domain.Entities.Activities;
using Rush.Domain.Entities.Audit;
using Rush.Domain.Entities.Auth;
using Rush.Domain.Entities.Configurations;
using Rush.Domain.Entities.Employees;
using Rush.Domain.Entities.ProjectResources;
using Rush.Domain.Entities.Projects;
using Rush.Domain.Entities.Resources;
using Rush.Domain.Entities.TaskEmployees;
using Task = Rush.Domain.Entities.Tasks.Task;

namespace Rush.Infraestructure.Common
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUser> AppUsers { get; set; }        
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ProjectResource> ProjectResources { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<TaskEmployees> TaskEmployees {get; set;}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>()
                .Property(p => p.Status)
                .HasConversion<int>(); 
            
            var entitiesAssembly = typeof(BaseEntity).Assembly;
            modelBuilder.RegisterAllEntities<BaseEntity>(entitiesAssembly);
        }
        
    }
}
