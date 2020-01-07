using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.ApiModels
{
    public class ApiResponse
    {
        public string Error { get; set; } = "Error: ";
        public ApiAuth auth { get; set; }
    }
}
