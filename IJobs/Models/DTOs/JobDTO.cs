using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models.DTOs
{
    public class JobDTO
    {
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public int Salary { get; set; }
        public string JobType { get; set; }
        public string Experience { get; set; }
        public bool Open { get; set; }
    }
}
