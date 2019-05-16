using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServiceDB.ORM.Generic;

namespace WebServiceDB.ORM.Application
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public testEntities MyAppContext
        {
            get { return Context as testEntities; }
        }

        public CourseRepository(testEntities context) : base(context)
        {
        }

        public IEnumerable<Course> GetCoursesWithAuthors(int pageIndex, int pageSize)
        {
            return MyAppContext.Courses.Include("User")
                .OrderBy(c => c.CreatedDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();
        }

        public IEnumerable<Course> GetTopSellingCourses(int count)
        {
            return MyAppContext.Courses.Take(count).ToList();
        }
    }
}