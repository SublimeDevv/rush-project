using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rush.Domain.Common.ViewModels.Admin;
using Rush.Domain.Common.ViewModels.Util;

namespace Rush.Application.Interfaces.Admin
{
    public interface IAdminService
    {
        public Task<ResponseHelper> GetAdminDashboardData();

    }
}
