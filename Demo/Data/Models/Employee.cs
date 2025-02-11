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
        [Range(18, 58)]
        public int? Age { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [InverseProperty(nameof(Department.Employees))]
        public Department? Department { get; set; }  // Navigational Propert {One}
        [ForeignKey(nameof(Department))]
        public int DeptId { get; set; }
    }
}
