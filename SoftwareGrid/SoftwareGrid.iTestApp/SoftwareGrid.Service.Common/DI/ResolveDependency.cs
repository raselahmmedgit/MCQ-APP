﻿using System.Web.Mvc;
using Ninject;
using SoftwareGrid.Repository.Base;
using SoftwareGrid.Repository.iTestApp.QuestionManagement;
using SoftwareGrid.Repository.iTestApp.TestManagement;
using SoftwareGrid.Repository.Security.UserManagement;
using SoftwareGrid.Service.Base;
using SoftwareGrid.Service.iTestApp.QuestionManagement;
using SoftwareGrid.Service.iTestApp.TestManagement;
using SoftwareGrid.Service.Security.UserManagement;

namespace SoftwareGrid.Service.Common.DI
{
    public class ResolveDependency
    {
        public void Resolve()
        {
            const string paramName = "dbContext";
            var kernel = new StandardKernel();
            var dbContext = new DbContext();

            #region Security
            kernel.Bind(typeof(IUserRepository)).To(typeof(UserRepository)).WithConstructorArgument(paramName, dbContext);


            kernel.Bind(typeof(IUserService)).To(typeof(UserService)).WithConstructorArgument(paramName, dbContext);

            #endregion

            #region ITestApp
            kernel.Bind(typeof(IBaseService<>)).To(typeof(BaseService<>)).WithConstructorArgument(paramName, dbContext);
            kernel.Bind<IQuestionCategoryService>().To<QuestionCategoryService>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<IQuestionService>().To<QuestionService>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<ITestCategoryService>().To<TestCategoryService>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<ITestService>().To<TestService>().WithConstructorArgument(paramName, dbContext);
            
            kernel.Bind(typeof(IBaseRepository<>)).To(typeof(BaseRepository<>)).WithConstructorArgument(paramName, dbContext);
            kernel.Bind<IQuestionCategoryRepository>().To<QuestionCategoryRepository>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<IQuestionRepository>().To<QuestionRepository>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<IQuestionAnswerOptionRepository>().To<QuestionAnswerOptionRepository>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<ITestCategoryRepository>().To<TestCategoryRepository>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<ITestRepository>().To<TestRepository>().WithConstructorArgument(paramName, dbContext);
            kernel.Bind<ITestWiseQuestionRepository>().To<TestWiseQuestionRepository>().WithConstructorArgument(paramName, dbContext);
            #endregion

           



            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
