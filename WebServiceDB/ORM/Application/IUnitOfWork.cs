using System;

namespace WebServiceDB.ORM.Application
{
    public interface IUnitOfWork:IDisposable
    {
        ICourseRepository Courses { get; }
        int Complete();
    }
}
