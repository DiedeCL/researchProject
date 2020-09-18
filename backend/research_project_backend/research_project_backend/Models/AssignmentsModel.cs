using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace research_project_backend.Models
{
    public class AssignmentsModel
    {
        [Required]
        public int CompanyId { get; set; }
        public int Id { get; set; }
    }
}
