using Demo.Data.DataSeed;
using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            //var employee = (from e in dbContext.Employees
            //                where e.EmpId == 1
            //                select e).FirstOrDefault();
            //Console.WriteLine(employee?.EmpName?? "Not Found");
            //var department = (from d in dbContext.Departments
            //                  where d.DeptId == 1
            //                  select d).FirstOrDefault();
            //Console.WriteLine(department?.Name ?? "Not Found");
            #endregion
            #region Explicit Loading
            //var employee = (from e in dbContext.Employees
            //                where e.DepartmentDeptId == 1
            //                select e).FirstOrDefault();
            //if (employee != null)
            //{
            //    dbContext.Entry(employee).Reference(d => d.Department).Load();
            //    Console.WriteLine($"EmployeeName: {employee?.EmpName ?? "Not Found"} & Department: {employee.Department.Name}");
            //}
            //var department = (from d in dbContext.Departments
            //                  where d.DeptId == 1
            //                  select d).FirstOrDefault();
            //dbContext.Entry(employee).Reference(d => d.Department).Load();
            //if (department != null)
            //{
            //    dbContext.Entry(department).Collection(d => d.Employees).Load();
            //    Console.WriteLine(employee?.EmpId + employee.EmpName + department?.Name ?? "Not Found");
            //}
            #endregion
            #region Eager Loading
            //var employee = (from e in dbContext.Employees.Include(e => e.Department)
            //                where e.DeptId == 1
            //                select e).FirstOrDefault();
            //if (employee != null)
            //{
            //    Console.WriteLine(employee?.EmpName ?? "Not Found");
            //}
            //var department = (from d in dbContext.Departments.Include(d => d.Employees)
            //                  where d.DeptId == 1
            //                  select d).FirstOrDefault();
            //if (department != null)
            //{
            //    Console.WriteLine($"{department?.DeptId} & {department?.Name ?? "Not Found"}");
            //    foreach (var item in department.Employees)
            //    {
            //        //Console.WriteLine(item.EmpName);   
            //    }
            //}
            #endregion
            #region Lazy Loading
            //var employee = (from e in dbContext.Employees
            //                where e.DeptId == 1
            //                select e).FirstOrDefault();
            //if (employee != null)
            //{
            //    Console.WriteLine(employee?.EmpName ?? "Not Found");
            //}
            //var department = (from d in dbContext.Departments
            //                  where d.DeptId == 1
            //                  select d).FirstOrDefault();
            //if (department != null)
            //{
            //    Console.WriteLine($"{department?.DeptId} & {department?.Name ?? "Not Found"}");
            //    foreach (var item in department.Employees)
            //    {
            //        //Console.WriteLine(item.EmpName);   
            //    }
            //}
            #endregion
            #endregion
            #region LINQ Operators [Join]
            #region LINQ Inner Join
            // Inner Join: 
            //var result = from E in dbContext.Employees
            //             join D in dbContext.Departments
            //             on E.DepartmentDeptId equals D.DeptId
            //             select new
            //             {
            //                 D.DeptId,
            //                 D.Name,
            //                 E.EmpId,
            //                 E.EmpName
            //             };
            // Employees ==> Outer & Department ==> Inner
            //result = dbContext.Employees.Join(dbContext.Departments, e => e.DeptId, d => d.DeptId, (e, d) => new
            //{
            //    d.DeptId,
            //    d.Name,
            //    e.EmpId,
            //    e.EmpName
            //});
            //foreach (var item in result)
            //{
            //Console.WriteLine(item);
            //}

            // Group Join: 
            //var result = dbContext.Departments.GroupJoin(dbContext.Employees, d => d.DeptId, e => e.DepartmentDeptId,
            //    (dept, emp) => new
            //    {
            //        Department = dept,
            //        Employees = emp
            //    }).Where(z => z.Employees.Count() > 0);
            //result = (from d in dbContext.Departments
            //          join e in dbContext.Employees
            //          on d.DeptId equals e.DepartmentDeptId
            //          into z
            //          select new
            //          {
            //              Department = d,
            //              Employees = z,
            //          }).Where(z => z.Employees.Count() > 0);
            //foreach (var item in result)
            //{
            //    Console.WriteLine(item.Department.Name);
            //    foreach (var item1 in item.Employees)
            //    {
            //        Console.WriteLine(item1.EmpName);
            //    }
            //}
            #endregion
            #region Left Outer Join
            //var result = from E in dbContext.Departments
            //             join D in dbContext.Employees
            //             on E.DepartmentDeptId equals D.DeptId
            //             select new
            //             {
            //                 D.DeptId,
            //                 D.Name,
            //                 E.EmpId,
            //                 E.EmpName
            //             };
            //Employees ==> Outer & Department ==> Inner
            var result = dbContext.Departments.GroupJoin(dbContext.Employees,
                 d => d.DeptId, e => e.DepartmentDeptId, (departments, employees) => new
                 {
                     departments,
                     employees = employees.DefaultIfEmpty()  //Null
                 }).SelectMany(z => z.employees, (z, Employee) => new
                 {
                     z.departments,
                     Employee
                 });
            var result1 = from d in dbContext.Departments
                          join e in dbContext.Employees
                          on d.DeptId equals e.DepartmentDeptId
                          into emp
                          select new
                          {
                              d,
                              emp = emp.DefaultIfEmpty()
                          } into x
                          from e in x.emp
                          select new
                          {
                              x.d,
                              e
                          };
            foreach (var item in result1)
            {
                Console.WriteLine($"{item.d.Name} & {item.e?.EmpName ?? "NotFound"}");
            }
            #endregion
            #endregion
        }
    }
}
