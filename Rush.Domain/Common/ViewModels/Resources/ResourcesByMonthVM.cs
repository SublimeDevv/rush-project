using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rush.Domain.Common.ViewModels.Resources
{
    public class ResourcesByMonthVM
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int ResourceQuantityMonth { get; set; }
    }
}
