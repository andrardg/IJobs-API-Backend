﻿using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.CompanyRepository
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        List<Company> GetByTitle(string title);
        List<Company> GetByTitleIncludingJobs(string title);
        List<Company> GetAllWithInclude();
        List<Company> GetAllWithJoin();
    }
}
