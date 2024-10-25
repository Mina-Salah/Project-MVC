using DEMO.DAL.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DEMO.PL.Models
{
    public class Employeeviewmodle
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

        public IFormFile Image { get; set; }
        public string? imageURL { get; set; }
        public int DepartmentId { get; set; }


    }
}
