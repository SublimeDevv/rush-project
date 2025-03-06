using AutoMapper;
using Rush.Application.Interfaces.Ejemplo;
using Rush.Application.Services.Base;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Domain.DTO.Ejemplo;
using Rush.Domain.Entities.Ejemplo;
using Rush.Infraestructure.Interfaces.Ejemplo;

namespace Rush.Application.Services.Ejemplo
{
    public class EjemploService: ServiceBase<EjemploTabla, EjemploTablaDTO>, IEjemploService
    {
        private readonly IMapper _mapper;
        private readonly IEjemploRepository _repository;

        public EjemploService(IEjemploRepository repository, IMapper mapper) : base(mapper, repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseHelper> GetByNombreAsync(string nombre)
        {
            ResponseHelper response = new();
            try
            {
                var getNombre = await _repository.GetByNombreAsync(nombre);
                if (getNombre != null)
                {
                    response.Success = true;
                    response.Data = getNombre;
                    response.Message = "Nombre encontrado";
                }

            } catch(Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }
    }
}
