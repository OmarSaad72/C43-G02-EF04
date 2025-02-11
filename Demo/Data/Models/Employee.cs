using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpId { get; set; }
        [StringLength(50, MinimumLength = 10)]
        public string EmpName { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal Salary { get; set; }
        /******************************Department Relation (Work 1: M)******************************/
        [InverseProperty(nameof(Models.Department.Employees))]
        public virtual Department? Department { get; set; } //Navigational Property ==> One Side
        [ForeignKey(nameof(Employee.Department))]
        public int? DepartmentDeptId { get; set; }
        /******************************Department Relation (Manage 1: 1)******************************/
        [InverseProperty(nameof(Models.Department.Manager))]
        public virtual Department? DepartmentManage { get; set; }
        [ForeignKey(nameof(DepartmentManage))]
        public int? DeptManageId { get; set; }
    }
}
