using Microsoft.AspNetCore.Identity;
using Rush.Domain.Entities;
using Rush.Infraestructure;
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

            if (!_context.Users.Any())
            {
                var users = new[]
                {
                    new ApplicationUser { UserName = "root@admin.com", Email = "root@admin.com", IsDeleted = false },
                    new ApplicationUser { UserName = "supervisor@gmail.com", Email = "supervisor@gmail.com", IsDeleted = false },
                    new ApplicationUser { UserName = "gerente@test.com", Email = "gerente@test.com", IsDeleted = false },
                    new ApplicationUser { UserName = "rh@gmail.com", Email = "rh@gmail.com", IsDeleted = false },
                    new ApplicationUser { UserName = "sistemas@gmail.com", Email = "sistemas@gmail.com", IsDeleted = false },
                    new ApplicationUser { UserName = "carlos@gmail.com", Email = "carlos@gmail.com", IsDeleted = false },
                    new ApplicationUser { UserName = "luis@gmail.com", Email = "luis@gmail.com", IsDeleted = false },
                    new ApplicationUser { UserName = "merida@gmail.com", Email = "merida@gmail.com", IsDeleted = false },
                    new ApplicationUser { UserName = "jaime@gmail.com", Email = "jaime@gmail.com", IsDeleted = false },
                    new ApplicationUser { UserName = "alison@gmail.com", Email = "alison@gmail.com", IsDeleted = false },
                    new ApplicationUser { UserName = "cochi@gmail.com", Email = "cochi@gmail.com" , IsDeleted = false },
                    new ApplicationUser { UserName = "david@gmail.com", Email = "davida@gmail.com", IsDeleted = false },
                    new ApplicationUser { UserName = "erika@gmail.com", Email = "erika@gmail.com", IsDeleted = false },
                    new ApplicationUser { UserName = "ocelot@gmail.com", Email = "ocelot@gmail.com", IsDeleted = false }
                };

                foreach (var user in users)
                {
                    var result = await _userManager.CreateAsync(user, "Niux123?");
                    if (result.Succeeded)
                    {
                        messages.Add($"Usuario {user.UserName} creado exitosamente.");

                        if (user.UserName == "root@admin.com")
                            await _userManager.AddToRoleAsync(user, "Admin");
                        else if (user.UserName == "supervisor@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Supervisor");
                        else if (user.UserName == "gerente@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Gerente");
                        else if (user.UserName == "rh@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Recursos Humanos");
                        else if (user.UserName == "sistemas@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Sistemas");
                        else if (user.UserName == "luis@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Empleado");
                        else if (user.UserName == "carlos@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Empleado");
                        else if (user.UserName == "merida@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Empleado");
                        else if (user.UserName == "jaime@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Empleado");
                        else if (user.UserName == "alison@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Empleado");
                        else if (user.UserName == "efrain@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Empleado");
                        else if (user.UserName == "david@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Empleado");
                        else if (user.UserName == "erika@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Empleado");
                        else if (user.UserName == "karen@gmail.com")
                            await _userManager.AddToRoleAsync(user, "Empleado");


                    }
                    else
                    {
                        messages.Add($"Error al crear el usuario {user.UserName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }


            //if (!_context.Categorias.Any())
            //{
            //    var categorias = new[]
            //    {
            //        new Categoria { Nombre = "Desarrollo Web" },
            //        new Categoria { Nombre = "SEO" },
            //        new Categoria { Nombre = "Marketing Digital" },
            //        new Categoria { Nombre = "Diseño Gráfico" },
            //        new Categoria { Nombre = "Desarrollo de Apps" },
            //        new Categoria { Nombre = "Consultoría" },
            //        new Categoria { Nombre = "Redes Sociales" },
            //        new Categoria { Nombre = "Publicidad" },
            //        new Categoria { Nombre = "E-commerce" },
            //        new Categoria { Nombre = "Branding" },
            //        new Categoria { Nombre = "Diseño Web" }

            //    };

            //    _context.Categorias.AddRange(categorias);
            //    _context.SaveChanges();
            //}

           

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
