using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebServiceDB.ORM.Generic;

namespace WebServiceDB.ORM.Application
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public testEntities MyAppContext
        {
            get { return Context as testEntities; }
        }

        public UserRepository(testEntities context) : base(context)
        {
        }
    }
}