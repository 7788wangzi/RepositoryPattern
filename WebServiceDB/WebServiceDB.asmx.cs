using Newtonsoft.Json;
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

                var courses = unitOfWork.Courses.GetLatestCourses(1, 10);

                //update example
                course.Abstract = "course 1 - abstract";
                unitOfWork.Complete();

                return string.Empty;
            }

        }

        [WebMethod]
        public string GetAllCourses()
        {
            using (var unitOfWork = new ORM.Application.UnitOfWork(new testEntities()))
            {
                var allCourses = unitOfWork.Courses.GetAll();

                
                string result = SerializeHelper.ToString(allCourses);

                return result;
            }
        }

        [WebMethod]
        public string GetCoursesOfAuthor(int authorID)
        {
            using (var unitOfWork = new ORM.Application.UnitOfWork(new testEntities()))
            {
                var authroCourses = unitOfWork.Courses.GetCoursesByAuthor(authorID);

                string result = SerializeHelper.ToString(authroCourses);

                return result;
            }
        }

        [WebMethod(MessageName = "Register", Description = "Register a new account")]
        public string Register(string UserName, string Password, string role, string phone)
        {
            PasswordHelper passwordHelper = new PasswordHelper();
            string encryptedPassword = passwordHelper.Encrypt(Password, "123");

            using (var unitOfWork = new ORM.Application.UnitOfWork(new testEntities()))
            {
                User newUser = new User()
                {
                    Name = UserName,
                    Password = encryptedPassword,
                    Role = role,
                    Telephone = phone
                };

                unitOfWork.Users.Add(newUser);
                unitOfWork.Complete();

                return SerializeHelper.ToString(newUser);
            }
        }

        [WebMethod(MessageName = "SignInWithPassword", Description = "Sign in with password")]
        //[System.Xml.Serialization.XmlInclude(typeof(int))]
        public string SignInWithPassword(string UserName, string Password)
        {
            PasswordHelper passwordHelper = new PasswordHelper();
            string encryptedPassword = passwordHelper.Encrypt(Password, "123");

            using (var unitOfWork = new ORM.Application.UnitOfWork(new testEntities()))
            {
                foreach(var user in unitOfWork.Users.GetAll())
                {
                    if(user.Name == UserName && user.Password == encryptedPassword)
                    {
                        return SerializeHelper.ToString(user);
                    }
                }
                return JsonConvert.SerializeObject("FAIL");
            }
        }

        [WebMethod(MessageName = "SignInWithTempCode", Description = "Sign in with temporary code")]
        //[System.Xml.Serialization.XmlInclude(typeof(int))]
        public string SignInWithTempCode(string phone, string TempCode)
        {
            using (var unitOfWork = new ORM.Application.UnitOfWork(new testEntities()))
            {
                foreach (var user in unitOfWork.Users.GetAll())
                {
                    if (user.Telephone == phone && user.TempCode == TempCode)
                    {
                        return SerializeHelper.ToString(user);
                    }
                }
                return JsonConvert.SerializeObject("FAIL");
            }
        }

        [WebMethod(MessageName = "ResetUserPassword", Description = "Reset user password")]
        //[System.Xml.Serialization.XmlInclude(typeof(int))]
        public string ResetUserPassword(int UserId, string passowrd)
        {
            PasswordHelper passwordHelper = new PasswordHelper();
            string encryptedPassword = passwordHelper.Encrypt(passowrd, "123");

            using (var unitOfWork = new ORM.Application.UnitOfWork(new testEntities()))
            {
                var user = unitOfWork.Users.Get(UserId);
                user.Password = encryptedPassword;
                unitOfWork.Complete();

                return SerializeHelper.ToString(user);
            }
        }
    }
}
