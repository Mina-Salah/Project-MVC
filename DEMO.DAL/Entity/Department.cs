using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.DAL.Entity
{
    public class Department : BaseEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100, ErrorMessage = "Department name must not exceed 100 characters.")]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
