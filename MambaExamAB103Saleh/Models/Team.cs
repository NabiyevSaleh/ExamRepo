using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MambaExamAB103Saleh.Models
{
    public class Team
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Profession { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
