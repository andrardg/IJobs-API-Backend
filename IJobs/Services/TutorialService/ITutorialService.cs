using IJobs.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Services.TutorialService
{
    public interface ITutorialService
    {
        IEnumerable<TutorialDTO> GetAllTutorials();
        IEnumerable<TutorialDTO> GetAllTutorialsJoin();
        IEnumerable<TutorialDTO> GetAllTutorialsBySubdomainId(Guid? id);
        IEnumerable<TutorialDTO> GetAllTutorialsBySubdomainIdJoin(Guid? id);
        TutorialDTO GetById(Guid? id);
        TutorialDTO GetByIdWithJoin(Guid? id);
        void Create(TutorialDTO model);
        void Update(TutorialDTO model);
        bool Save();
        void Delete(Guid? id);
    }
}
