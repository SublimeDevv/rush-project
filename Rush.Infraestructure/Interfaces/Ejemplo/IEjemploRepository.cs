using Rush.Domain.Common.ViewModels.Ejemplo;
using Rush.Domain.Entities.Ejemplo;
using Rush.Infraestructure.Repositories.Generic;

namespace Rush.Infraestructure.Interfaces.Ejemplo
{
    public interface IEjemploRepository : IBaseRepository<EjemploTabla>
    {
        Task<EjemploVM> GetByNombreAsync(string nombre);
    }
}