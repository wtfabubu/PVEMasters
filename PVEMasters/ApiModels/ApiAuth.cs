using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.ApiModels
{
    public class ApiAuth
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public int AccLvl { get; set; }
    }
}
