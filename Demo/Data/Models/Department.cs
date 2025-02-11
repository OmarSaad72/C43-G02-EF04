using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }
        public string? Name { get; set; }

        //Works ==> 1 : M
        [InverseProperty(nameof(Employee.Department))]
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>(); //   ==> Side Many

        //Manage ==> 1 : 1
        [InverseProperty(nameof(Employee.DepartmentManage))]
        public virtual Employee Manager { get; set; }
    }
}
