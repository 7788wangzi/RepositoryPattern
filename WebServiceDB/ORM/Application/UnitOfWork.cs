using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceDB.ORM.Application
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly testEntities _context;
        public ICourseRepository Courses { get; private set; }
        public IUserRepository Users { get; private set; }

        public UnitOfWork(testEntities entities)
        {
            _context = entities;
            Courses = new CourseRepository(_context);
            Users = new UserRepository(_context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}