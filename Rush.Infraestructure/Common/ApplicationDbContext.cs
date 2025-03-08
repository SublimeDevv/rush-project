using System.Runtime.InteropServices.ComTypes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rush.Domain.Entities;
using Rush.Domain.Entities.Ejemplo;

namespace Rush.Infraestructure.Common
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> AppUsers { get; set; }        
        public DbSet<Project> Projects { get; set; }
        
        public DbSet<EmployeesTasks> EmployeesTasks { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Activities> Activities { get; set; }
        
        public DbSet<ProjectResources> ProjectResources { get; set; }
        public DbSet<Resources> Resources { get; set; }
        public DbSet<Employees> Employees { get; set; }
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            var entitiesAssembly = typeof(BaseEntity).Assembly;
            modelBuilder.RegisterAllEntities<BaseEntity>(entitiesAssembly);
        }
    }
}
