using System.ComponentModel.DataAnnotations;

namespace Walks.API.Models.DTOs
{
    public class LoginRequestDto
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty; 
    }
}
