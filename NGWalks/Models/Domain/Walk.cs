namespace NGWalks.Models.Domain
{
    public class Walk
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DiffcultyId { get; set; }
        public Guid RegionId { get; set; }
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
    }
}
