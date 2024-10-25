using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.DAL.Entity
{
    public class Employee: BaseEntity
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Column(TypeName = "Money")]
        public double Salary { get; set; }
        public bool IsActive { get; set; }
        public DateTime HireDate { get; set; } = DateTime.Now;

        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public string imgUrl { get; set; }  
    }
}
