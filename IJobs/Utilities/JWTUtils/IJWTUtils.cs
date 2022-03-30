using IJobs.Models;
using IJobs.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Utilities.JWTUtils
{
    public interface IJWTUtils<TEntity> where TEntity : BaseEntity
    {
        public string GenerateJWTToken(TEntity user, Role role);
        public Guid ValidateJWTToken(string token);
    }
}
