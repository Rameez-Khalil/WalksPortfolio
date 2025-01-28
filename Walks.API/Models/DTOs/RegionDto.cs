using System.ComponentModel.DataAnnotations;

namespace Walks.API.Models.DTOs
{
    public class RegionDto
    {
        [Required]
        [MinLength(3, ErrorMessage ="It has to be of min 3 characters")]
        [MaxLength(100, ErrorMessage ="It has to be of max 100 characters")]
        public string Name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage="Code has to be min of 3 characters")]
        [MaxLength(5, ErrorMessage ="Code has to be max of 5 characters")]
        public string code { get; set; }
        public string? RegionImageUrl { get; set; }

    }
}
