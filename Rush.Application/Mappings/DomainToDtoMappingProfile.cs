using AutoMapper;
using Rush.Domain.Entities;
using Rush.Domain.DTO;
using Rush.Domain.Entities.Ejemplo;
using Rush.Domain.DTO.Ejemplo;


namespace Rush.Application.Mappings
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            this.CreateMap<BaseEntity, BaseDTO>();
            this.CreateMap<EjemploTabla, EjemploTablaDTO>().ReverseMap();
        }
    }
}
