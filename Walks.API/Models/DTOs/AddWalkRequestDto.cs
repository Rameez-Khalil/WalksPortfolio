using System.ComponentModel.DataAnnotations;

namespace Walks.API.Models.DTOs
{
    public class AddWalkRequestDto
    {

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]

        public string Description { get; set; }

        [Required]
        [MinLength(3)]
        public string LengthInKms { get; set; }

        [Required]
        [MinLength(3)]
        public string WalkImageUrl { get; set; }

        public Guid RegionId { get; set; }
        public Guid DifficultyId { get; set; }
    }
}
