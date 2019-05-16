using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;

namespace WebServiceDB
{
    /// <summary>
    /// Summary description for WebServiceDB
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceDB : System.Web.Services.WebService
    {
        [WebMethod]
        public string TestORMRepositoryPattern()
        {
            using (var unitOfWork = new ORM.Application.UnitOfWork(new testEntities()))
            {
                //get example
                var course = unitOfWork.Courses.Get(1);
                var allCourses = unitOfWork.Courses.GetAll();             

                var courses = unitOfWork.Courses.GetCoursesWithAuthors(1, 10);

                //update example
                course.Abstract = "course 1 - abstract";
                unitOfWork.Complete();

                return string.Empty;
            }

        }


        //[WebMethod]
        //public string HelloWorld()
        //{
           

        //    testEntities myEntity = new testEntities();

        //    myEntity.Users.Add(new User()
        //    {
        //        Name = "user 2",
        //        Password = "456",
        //        CreatedDate = System.DateTime.UtcNow
        //    });
        //    var result = myEntity.SaveChanges();

        //    return result.ToString();
        //}

        //[WebMethod(MessageName = "Register", Description = "Register a new account")]
        ////[System.Xml.Serialization.XmlInclude(typeof(int))]
        //public async Task<int> Register(string UserName, string Password)
        //{
        //    testEntities myEntity = new testEntities();

        //    myEntity.Users.Add(new User()
        //    {
        //        Name = UserName,
        //        Password = Password,
        //        CreatedDate = System.DateTime.UtcNow
        //    });
        //    var result = await myEntity.SaveChangesAsync();
        //    return result;
        //}

        //[WebMethod(MessageName = "GetUsers", Description = "Get all users")]
        //public List<User> GetUsers()
        //{
        //    testEntities myEntity = new testEntities();
        //    return myEntity.Users.ToList<User>();
        //}
    }
}
