using Dapper;
using Microsoft.EntityFrameworkCore;
using Rush.Domain.Common.ViewModels.Ejemplo;
using Rush.Domain.Entities.Ejemplo;
using Rush.Infraestructure.Interfaces.Ejemplo;
using Rush.Infraestructure.Repositories.Generic;

namespace Rush.Infraestructure.Repositories.Ejemplo
{
    public class EjemploRepository : BaseRepository<EjemploTabla>, IEjemploRepository
    {
        private readonly ApplicationDbContext _context;
        public EjemploRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<EjemploVM> GetByNombreAsync(string nombre)
        {
            string sql = "SELECT Nombre AS NombreCompleto FROM Tbl_Ejemplo WHERE Nombre = @nombre";
            var result = await _context.Database.GetDbConnection().QueryFirstAsync<EjemploVM>(sql, new { nombre });
            return result;

        }
    }
}