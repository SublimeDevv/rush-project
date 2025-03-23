using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rush.Domain.Common.ViewModels.Admin;
using Rush.Infraestructure.Repositories.Generic;

namespace Rush.Infraestructure.Interfaces.Admin
{
    public interface IAdminRepository
    {
        public Task<AdminDashboardDataVM> GetAdminDashboardData();

    }
}
