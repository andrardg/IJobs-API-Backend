using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IJobs.Models.Base
{
    public class BaseEntity: IBaseEntity
    {
        [Key]
        //generate a value when a row is inserted
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid Id { get; set; }
        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }
        /*public BaseEntity()
        {
            this.DateCreated = DateTime.UtcNow;
            this.DateModified = DateTime.UtcNow;
        }*/
    }
    
}
