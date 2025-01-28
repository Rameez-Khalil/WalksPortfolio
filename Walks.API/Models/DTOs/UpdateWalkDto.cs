using Walks.API.Models.Domain;

namespace Walks.API.Models.DTOs
{
    public class UpdateWalkDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LengthInKms { get; set; }
        public string WalkImageUrl { get; set; }

        public Guid RegionId{ get; set; }
        public Guid DifficultyId { get; set; }
    }
}
