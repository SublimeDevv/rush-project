using AutoMapper;
using Rush.Application.Interfaces.Activities;
using Rush.Application.Services.Base;
using Rush.Domain.Common.ViewModels.Activities;
using Rush.Domain.Common.ViewModels.Tasks;
using Rush.Domain.Common.ViewModels.Util;
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

        public async Task<ResponseHelper> GetEmployeeActivities(Guid EmployeeId)
        {
            ResponseHelper response = new();

            try
            {
                List<ActivityVM> activities = await _repository.GetEmployeeActivities(EmployeeId);

                if (activities.Count == 0)
                {

                    response.Message = "No se encontraron las actividades";
                    response.Success = false;
                }

                response.Data = activities;
                response.Success = true;


            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

    }
}
