using AutoMapper;
using IJobs.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<UserRequestDTO, User>(); // means you want to map from  UserRequestDTO to User
            CreateMap<User, UserResponseDTO>();
            CreateMap<Job, JobDTO>();
            CreateMap<CompanyRequestDTO, Company>();
            CreateMap<Company, CompanyResponseDTO>();
            CreateMap<UserJobRelation, UserJobRelationDTO>();
        }
    }
}