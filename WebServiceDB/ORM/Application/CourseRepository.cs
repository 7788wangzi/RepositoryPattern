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

        public IEnumerable<Course> GetLatestCourses(int pageIndex, int pageSize)
        {
            return MyAppContext.Courses
                .Where(c => c.IsApproved == true && c.IsPublic == true)
                .OrderBy(c => c.CreatedDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();
        }

        public IEnumerable<Course> GetLatestCourses(int count)
        {
            return MyAppContext.Courses
                .Where(c => c.IsApproved == true && c.IsPublic == true)
                .OrderBy(c => c.CreatedDate)
                .Take(count).ToList();
        }

        public IEnumerable<Course> GetCoursesByAuthor(int AuthorId)
        {
            return MyAppContext.Courses
                .Where(c => (c.Author==AuthorId && c.IsApproved == true && c.IsPublic == true)).ToList();
        }
    }
}