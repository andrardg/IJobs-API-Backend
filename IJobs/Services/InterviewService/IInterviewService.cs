using IJobs.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Services.InterviewService
{
    public interface IInterviewService
    {
        IEnumerable<InterviewDTO> GetAllInterviews();
        IEnumerable<InterviewDTO> GetAllInterviewsJoin();
        InterviewDTO GetById(Guid? id);
        InterviewDTO GetByIdWithJoin(Guid? id);
        void Create(InterviewDTO model);
        void Update(InterviewDTO model);
        bool Save();
        void Delete(Guid? id);
    }
}
