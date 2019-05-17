using System;

namespace WebServiceDB.ORM.Application
{
    public interface IUnitOfWork:IDisposable
    {
        ICourseRepository Courses { get; }
        IUserRepository Users { get; }
        int Complete();
    }
}
