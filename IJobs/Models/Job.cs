﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models
{
    public class Job : Base.BaseEntity
    {
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public int Salary { get; set; }
        public string JobType { get; set; }
        public string Experience { get; set; }
        public bool Open { get; set; }
        public Company Company { get; set; } // one to many between job and company
        public Guid CompanyId { get; set; }
        public ICollection<UserJobRelation> UserJobRelations { get; set; } //many to many between job and user
    }
}
