using AutoMapper;
using Rush.Application.Interfaces.Activities;
using Rush.Application.Services.Base;
using Rush.Domain.DTO.Activities;
using Rush.Domain.Entities.Activities;
using Rush.Infraestructure.Interfaces.Activities;

namespace Rush.Application.Services.Activities
{
    public class ActivityService: ServiceBase<Activity, ActivityDTO>, IActivityService
    {
        private readonly IMapper _mapper;
        private readonly IActivityRepository _repository;

        public ActivityService(IActivityRepository repository, IMapper mapper) : base(mapper, repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        
    }
}
