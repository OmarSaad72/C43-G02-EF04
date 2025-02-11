using Demo.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demo.Data.DataSeed
{
    public static class CompanyDbContextSeed
    {
        public static void Seed(CompanyDbContext dbContext)
        {
            var DepartmentFile = File.ReadAllText("C:\\Users\\OmarSaad\\source\\repos\\Session04_EFCore\\Demo\\Data\\DataSeed\\departments.json");
            var departments = JsonSerializer.Deserialize <List<Department>>(DepartmentFile);
            if (!dbContext.Departments.Any())
            {
                if (departments.Count() > 0)
                {
                    foreach (var department in departments)
                    {
                        dbContext.Departments.Add(department);
                    }
                    dbContext.SaveChanges();
                }
            }
            var EmployeeFile = File.ReadAllText("C:\\Users\\OmarSaad\\source\\repos\\Session04_EFCore\\Demo\\Data\\DataSeed\\employees.json");
            var employees = JsonSerializer.Deserialize <List<Employee>>(EmployeeFile);
            if (!dbContext.Employees.Any())
            {
                if (employees.Count() > 0)
                {
                    foreach (var employee in employees)
                    {
                        dbContext.Employees.Add(employee);
                    }
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
