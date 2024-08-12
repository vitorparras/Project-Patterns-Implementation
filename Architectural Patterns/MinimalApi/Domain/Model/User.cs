using Domain.Model.Bases;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class User : BaseEntity
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        
        [Required] 
        public string Password { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }
    }
}
