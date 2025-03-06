using Rush.Application.Interfaces.Base;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.Ejemplo;
using Rush.Domain.Entities.Ejemplo;

namespace Rush.Application.Interfaces.Ejemplo
{
    public interface IEjemploService: IServiceBase<EjemploTabla, EjemploTablaDTO>
    {
        Task<ResponseHelper> GetByNombreAsync(string nombre);
    }
}
