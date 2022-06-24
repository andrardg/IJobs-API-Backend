using AutoMapper;
using IJobs.Data;
using IJobs.Models;
using IJobs.Models.DTOs;
using IJobs.Repositories.InviteRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Services.InviteService
{
    public class InviteService : IInviteService
    {
        public IInviteRepository _inviteRepository;
        public projectContext _context;
        private readonly IMapper _mapper;
        public InviteService(IInviteRepository inviteRepository, IMapper mapper, projectContext context)
        {
            _inviteRepository = inviteRepository;
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<InviteDTO> GetAllInvites()
        {
            var results = _inviteRepository.GetAll();
            var dtos = new List<InviteDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<InviteDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }

        public IEnumerable<InviteDTO> GetAllInvitesJoin()
        {
            var results = _inviteRepository.GetAllJoin();
            var dtos = new List<InviteDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<InviteDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }

        public InviteDTO GetById(Guid? id)
        {
            var result = _inviteRepository.GetById(id);
            var app = _mapper.Map<InviteDTO>(result);
            return app;
        }

        public InviteDTO GetByIdWithJoin(Guid? id)
        {
            var result = _inviteRepository.GetByIdJoin(id);
            var app = _mapper.Map<InviteDTO>(result);
            return app;
        }
        public void Create(InviteDTO model)
        {
            var inv = _mapper.Map<Invite>(model);
            inv.Id = Guid.NewGuid();
            inv.DateCreated = DateTime.UtcNow;
            inv.DateModified = DateTime.UtcNow;
            _inviteRepository.Create(inv);
            Save();
        }
        public void Delete(Guid? id)
        {
            var inv = _inviteRepository.GetById(id);
            _inviteRepository.Delete(inv);
        }
        public bool Save()
        {
            try
            {
                return _inviteRepository.Save() != false;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
    }
}
