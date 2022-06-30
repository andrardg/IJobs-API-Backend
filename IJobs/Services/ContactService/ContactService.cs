using AutoMapper;
using IJobs.Data;
using IJobs.Models;
using IJobs.Models.DTOs;
using IJobs.Repositories.ContactRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Services.ContactService
{
    public class ContactService : IContactService
    {
        public IContactRepository _contactRepository;
        public projectContext _context;
        private readonly IMapper _mapper;
        public ContactService(IContactRepository contactRepository, IMapper mapper, projectContext context)
        {
            _contactRepository = contactRepository;
            _context = context;
            _mapper = mapper;
        }
        public void Create(ContactDTO model)
        {
            var contact = _mapper.Map<Contact>(model);
            contact.Id = Guid.NewGuid();
            contact.DateCreated = DateTime.UtcNow;
            contact.DateModified = DateTime.UtcNow;
            _contactRepository.Create(contact);
            Save();
        }

        public void Delete(Guid? id)
        {
            var contact = _contactRepository.GetById(id);
            _contactRepository.Delete(contact);
        }

        public IEnumerable<ContactDTO> GetAll()
        {
            var results = _contactRepository.GetAll();
            var dtos = new List<ContactDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<ContactDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }

        public bool Save()
        {
            try
            {
                return _contactRepository.Save() != false;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return false;
        }

        public void Update(ContactDTO model)
        {
            var contact = _mapper.Map<Contact>(model);
            contact.DateModified = DateTime.UtcNow;
            _contactRepository.Update(contact);
        }
    }
}
