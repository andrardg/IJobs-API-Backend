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
            CreateMap<JobDTO, Job>();

            CreateMap<CompanyRequestDTO, Company>();
            CreateMap<Company, CompanyResponseDTO>();

            CreateMap<User, AccountDTO>();
            CreateMap<AccountDTO, User>();
            CreateMap<Company, AccountDTO>();
            CreateMap<AccountDTO, Company>();

            CreateMap<Domain, DomainDTO>();
            CreateMap<DomainDTO, Domain>();

            CreateMap<Subdomain, SubdomainDTO>();
            CreateMap<SubdomainDTO, Subdomain>();

            CreateMap<Tutorial, TutorialDTO>();
            CreateMap<TutorialDTO, Tutorial>();

            CreateMap<Application, ApplicationDTO>();
            CreateMap<ApplicationDTO, Application>();

            CreateMap<Interview, InterviewDTO>();
            CreateMap<InterviewDTO, Interview>();

            CreateMap<Invite, InviteDTO>();
            CreateMap<InviteDTO, Invite>();
        }
    }
}