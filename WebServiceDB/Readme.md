## Demo of repository pattern with C# and Entity Framework

Benifits
- Minimizes duplicate query logic
- Decouples your application from persistence frameworks, there will be a new ORM framework every 2 years.
- Promotes testability

What is a repository pattern
a repository pattern implements following methods to manuplicate data.
```
Add(obj)
Remove(obj)
Get(id)
GetAll()
Find(predicate)
```

**Minimize duplicate query logic**
In EF:
```
var topSellingCourses = context.Courses
.where(c => c.IsPublic  && c.IsApproved)
.OderByDescending(c => c.Sales)
.Take(10);
```

Repository:
```
var courses = repository.GetTopSellingCourses();
```

## Implement a Repository Pattern
![](ImplementRepositoryPattern.png)
Generic Code that are independent to our applications, which means different applications could leverage it without changing anything.

IRepository
```
Add()
Remove()
Get(id)
Find(predicate)
```

Repository implements IRepository. Repository has a property of DBContext.

Application associated code:
ICourseRepository inherits from IRepository, it defines some methods that special to our application besides the general add, get, etc. for example:
```
GetTopSellingCoures()
GetCoursesWithAuthors()
```

CourseRepository implements ICourseRepository and Repository<Course>, it ahs a propery of DBContext related to our application.


IUnitOfWork
```
ICourseRepository Courses{get;}
IAuthorRepository Authors{get;}
void Complete()
```
UnitOfWork implements IUnitOfWork