using AutoMapper;
using IJobs.Data;
using IJobs.Models;
using IJobs.Models.DTOs;
using IJobs.Repositories.TutorialRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Services.TutorialService
{
    public class TutorialService : ITutorialService
    {
        public ITutorialRepository _tutorialRepository;
        public projectContext _context;
        private readonly IMapper _mapper;
        public TutorialService(ITutorialRepository tutorialRepository, IMapper mapper, projectContext context)
        {
            _tutorialRepository = tutorialRepository;
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<TutorialDTO> GetAllTutorials()
        {
            var results = _tutorialRepository.GetAll();
            var dtos = new List<TutorialDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<TutorialDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }
        public IEnumerable<TutorialDTO> GetAllTutorialsBySubdomainId(Guid? id)
        {
            var results = _tutorialRepository.GetAllBySubdomainId(id);
            var dtos = new List<TutorialDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<TutorialDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }
        public IEnumerable<TutorialDTO> GetAllTutorialsBySubdomainIdJoin(Guid? id)
        {
            var results = _tutorialRepository.GetAllBySubdomainIdJoin(id);
            var dtos = new List<TutorialDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<TutorialDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }
        public TutorialDTO GetById(Guid? id)
        {
            var result = _tutorialRepository.GetById(id);
            var tutorial = _mapper.Map<TutorialDTO>(result);
            return tutorial;
        }
        public TutorialDTO GetByIdWithJoin(Guid? id)
        {
            var result = _tutorialRepository.GetByIdWithJoin(id);
            var tutorial = _mapper.Map<TutorialDTO>(result);
            return tutorial;
        }
        public void Create(TutorialDTO model)
        {
            var tutorial = _mapper.Map<Tutorial>(model);
            tutorial.Id = Guid.NewGuid();
            tutorial.DateCreated = DateTime.UtcNow;
            tutorial.DateModified = DateTime.UtcNow;
            _tutorialRepository.Create(tutorial);
            Save();
        }
        public void Delete(Guid? id)
        {
            var tutorial = _tutorialRepository.GetById(id);
            _tutorialRepository.Delete(tutorial);
        }
        public bool Save()
        {
            try
            {
                return _tutorialRepository.Save() != false;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
        public void Update(TutorialDTO model)
        {
            var tutorial = _mapper.Map<Tutorial>(model);
            tutorial.DateModified = DateTime.UtcNow;
            _tutorialRepository.Update(tutorial);
        }
    }
}
