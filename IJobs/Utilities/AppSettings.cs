using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Utilities
{
    public class AppSettings
    {
        public string JwtSecret { get; set; }
    }
}
