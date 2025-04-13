namespace NGWalks.Models.DTO
{
    public class UpdateWalksRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
