using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models
{
    public class Interview : Base.BaseEntity
    {
        public Guid ApplicationId { get; set; }
        public Application Application { get; set; }
        public DateTime Date { get; set; }
        public bool IsOnline { get; set; }
        public string Location { get; set; }
        public bool ResponseUser { get; set; }
        public bool ResponseCompany { get; set; }
        //if u reject -> delete inteview -> ApplicationStatus = rejected
        //if date not ok, alter it and the other's response is marked 0, yours = 1
        // 0 = in review, 1 = accept, 2 = reject
    }
}
