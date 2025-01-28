using Walks.API.Models.Domain;

namespace Walks.API.Models.DTOs
{
    public class WalkDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LengthInKms { get; set; }
        public string WalkImageUrl { get; set; }

        public Region Region { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
