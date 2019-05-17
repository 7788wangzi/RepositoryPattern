using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServiceDB.ORM.Generic;

namespace WebServiceDB.ORM.Application
{
    public interface ICourseRepository: IRepository<Course>
    {
        IEnumerable<Course> GetLatestCourses(int count);
        IEnumerable<Course> GetLatestCourses(int pageIndex, int pageSize);
        IEnumerable<Course> GetCoursesByAuthor(int AuthorId);
    }
}
