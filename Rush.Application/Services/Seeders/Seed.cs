using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rush.Domain.Entities;
using Rush.Domain.Entities.Activities;
using Rush.Domain.Entities.Employees;
using Rush.Domain.Entities.ProjectResources;
using Rush.Domain.Entities.Projects;
using Rush.Domain.Entities.Resources;
using Rush.Infraestructure.Common;
using static Rush.Domain.Common.Util.Enums;


namespace Rush.Application.Services.Seeders
{
    public class Seed
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Seed(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<List<string>> SeedDataAsync()
        {
            var messages = new List<string>();

            await CreateRolesAsync(messages);

            // 1. Crear 20 usuarios
            if (!_context.Users.Any())
            {
                var users = new List<ApplicationUser>();
                for (int i = 1; i <= 20; i++)
                {
                    users.Add(new ApplicationUser
                    {
                        UserName = $"user{i}@test.com",
                        Email = $"user{i}@test.com",
                        IsDeleted = false
                    });
                }

                // Agregar usuarios especiales
                users.AddRange(new[]
                {
            new ApplicationUser { UserName = "root@admin.com", Email = "root@admin.com", IsDeleted = false },
            new ApplicationUser { UserName = "superv    isor@gmail.com", Email = "supervisor@gmail.com", IsDeleted = false },
            new ApplicationUser { UserName = "gerente@test.com", Email = "gerente@test.com", IsDeleted = false },
            new ApplicationUser { UserName = "sistemas@test.com", Email = "sistemas@test.com", IsDeleted = false },
            new ApplicationUser { UserName = "recursoshumanos@test.com", Email = "recursoshumanos@test.com", IsDeleted = false }

        });

                foreach (var user in users)
                {
                    var result = await _userManager.CreateAsync(user, "Niux123?");
                    if (result.Succeeded)
                    {
                        var role = user.UserName switch
                        {
                            "root@admin.com" => "Admin",
                            "supervisor@gmail.com" => "Supervisor",
                            "gerente@test.com" => "Gerente",
                            "sistemas@test.com" => "Sistemas",
                            "recursoshumanos@test.com" => "Recursos Humanos",
                            _ => "Empleado"
                        };
                        await _userManager.AddToRoleAsync(user, role);
                        messages.Add($"Usuario {user.UserName} creado con rol {role}");
                    }
                }
            }

            // 2. Crear 20 proyectos
            if (!_context.Projects.Any())
            {
                var projects = new List<Project>();
                for (int i = 1; i <= 20; i++)
                {
                    projects.Add(new Project
                    {
                        Name = $"Proyecto {i}",
                        Description = $"Descripción del proyecto {i}",
                        StartDate = DateTime.Now.AddDays(-i * 10),
                        EndTime = DateTime.Now.AddDays(i * 20),
                        Status = (StatusProject)(i % Enum.GetNames(typeof(StatusProject)).Length)
                    });
                }
                _context.Projects.AddRange(projects);
                await _context.SaveChangesAsync();
                messages.Add("20 proyectos creados");
            }

            // 3. Crear 20 empleados con proyectos asignados
            if (!_context.Employees.Any())
            {
                var projects = await _context.Projects.ToListAsync();
                var users = await _userManager.Users.ToListAsync();

                var employees = new List<Employee>();
                for (int i = 0; i < 20; i++)
                {
                    employees.Add(new Employee
                    {
                        Name = $"Empleado {i + 1}",
                        LastName = $"Apellido {i + 1}",
                        Age = 25 + i,
                        Curp = $"CURP{i + 1}".PadRight(18, 'X'),
                        Rfc = $"RFC{i + 1}".PadRight(13, 'X'),
                        Salary = $"{15000 + (i * 1000)}.00",
                        UserId = users[i].Id,
                        ProjectId = projects[i % projects.Count].Id
                    });
                }
                _context.Employees.AddRange(employees);
                await _context.SaveChangesAsync();
                messages.Add("20 empleados creados con proyectos asignados");
            }

            // 4. Crear 20 recursos
            if (!_context.Resources.Any())
            {
                var resources = new List<Resource>();
                for (int i = 1; i <= 20; i++)
                {
                    resources.Add(new Resource
                    {
                        Name = $"Recurso {i}",
                        Description = $"Descripción del recurso {i}",
                        Quantity = i * 5
                    });
                }
                _context.Resources.AddRange(resources);
                await _context.SaveChangesAsync();
                messages.Add("20 recursos creados");
            }

            // 5. Crear 20 project resources
            if (!_context.ProjectResources.Any())
            {
                var projects = await _context.Projects.ToListAsync();
                var resources = await _context.Resources.ToListAsync();

                var projectResources = new List<ProjectResource>();
                for (int i = 0; i < 20; i++)
                {
                    projectResources.Add(new ProjectResource
                    {
                        ProjectId = projects[i].Id,
                        ResourceId = resources[i].Id,
                        Quantity = 10 + i,
                        UsedQuantity = 3 + i
                    });
                }
                _context.ProjectResources.AddRange(projectResources);
                await _context.SaveChangesAsync();
                messages.Add("20 project resources creados");
            }

            // 6. Crear 20 tareas
            if (!_context.Tasks.Any())
            {
                var projects = await _context.Projects.ToListAsync();
                var tasks = new List<Domain.Entities.Tasks.Task>();

                for (int i = 0; i < 20; i++)
                {
                    tasks.Add(new Domain.Entities.Tasks.Task
                    {
                        ProjectId = projects[i].Id,
                        Name = $"Tarea {i + 1}",
                        Description = $"Descripción de la tarea {i + 1}",
                        EstimatedHours = 40 + (i * 2),
                        WorkedHours = 20 + i,
                        StartDate = DateTime.Now.AddDays(-i * 2),
                        EndTime = DateTime.Now.AddDays(i * 3)
                    });
                }
                _context.Tasks.AddRange(tasks);
                await _context.SaveChangesAsync();
                messages.Add("20 tareas creadas");
            }

            // 7. Crear 20 actividades
            if (!_context.Activities.Any())
            {
                var tasks = await _context.Tasks.ToListAsync();
                var employees = await _context.Employees.ToListAsync();

                var activities = new List<Activity>();
                for (int i = 0; i < 20; i++)
                {
                    activities.Add(new Activity
                    {
                        TaskId = tasks[i].Id,
                        EmployeeId = employees[i].Id,
                        Name = $"Actividad {i + 1}",
                        Description = $"Descripción de la actividad {i + 1}",
                        Status = StatusActivity.BEGIN
                    });
                }
                _context.Activities.AddRange(activities);
                await _context.SaveChangesAsync();
                messages.Add("20 actividades creadas");
            }

            return messages;
        }

        private async Task CreateRolesAsync(List<string> messages)
        {
            string[] roleNames = { "Admin", "Gerente", "Supervisor", "Recursos Humanos", "Sistemas", "Empleado" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                    messages.Add($"Rol {roleName} creado exitosamente.");
                }
                else
                {
                    messages.Add($"El rol {roleName} ya existe.");
                }
            }
        }
    }
}
