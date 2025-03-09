using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rush.Domain.Entities;
using Rush.Domain.Entities.Activities;
using Rush.Domain.Entities.Employees;
using Rush.Domain.Entities.ProjectResources;
using Rush.Domain.Entities.Projects;
using Rush.Domain.Entities.Resources;
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
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            var entitiesAssembly = typeof(BaseEntity).Assembly;
            modelBuilder.RegisterAllEntities<BaseEntity>(entitiesAssembly);
        }
    }
}
