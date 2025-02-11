using Demo.Data.DataSeed;
using Demo.Data.Models;

namespace Demo
{
    internal class Program
    {
        static void Main()
        {
            #region Data Seeding
            using CompanyDbContext dbContext = new CompanyDbContext();
            //CompanyDbContextSeed.Seed(dbContext);
            #endregion
            #region Loading
            #region NavigationalProperty Loading
            var employee = (from e in dbContext.Employees
                           where e.DeptId == 1
                           select e).FirstOrDefault();
            //Console.WriteLine(employee?.EmpName??"Not Found");
            var department = (from d in dbContext.Departments
                              where d.DeptId == 1
                              select d).FirstOrDefault();
            //Console.WriteLine(department?.Name ?? "Not Found");
            #endregion
            #endregion
        }
    }
}
