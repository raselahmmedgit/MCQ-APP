﻿using System;
using SoftwareGrid.Model.iTestApp.UserManagement;
using SoftwareGrid.Model.iTestApp.Utility;
using SoftwareGrid.Repository.iTestApp.Base;
using SoftwareGrid.Repository.iTestApp.UserManagement;
using SoftwareGrid.Service.iTestApp.Base;

namespace SoftwareGrid.Service.iTestApp.UserManagement
{
    public class CompanyService : BaseService<Company>, ICompanyService
    {
        private readonly ICompanyRepository _iCompanyRepository;
        private readonly AppDbContext _dbContext;

        public CompanyService(IBaseRepository<Company> iBaseRepository, ICompanyRepository iCompanyRepository, AppDbContext dbContext)
            : base(iBaseRepository, dbContext)
        {
            _iCompanyRepository = iCompanyRepository;
            _dbContext = dbContext;
        }

    }

    public interface ICompanyService : IBaseService<Company>
    {
    }

}
