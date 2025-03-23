using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Rush.Application.Interfaces.Admin;
using Rush.Domain.Common.ViewModels.Util;
using Rush.Infraestructure.Interfaces.Admin;
using Rush.Infraestructure.Interfaces.Employees;

namespace Rush.Application.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _repository;
        private readonly IMapper _mapper;

        public AdminService(IAdminRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseHelper> GetAdminDashboardData()
        {
            ResponseHelper response = new();

            try
            {
                var adminDashboardData = await _repository.GetAdminDashboardData();

                if (adminDashboardData == null)
                {

                    response.Message = "No se encontraron los datos";
                    response.Success = false;
                }

                response.Data = adminDashboardData;
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
