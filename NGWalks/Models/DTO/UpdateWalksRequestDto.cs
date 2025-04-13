using System.ComponentModel.DataAnnotations;

namespace NGWalks.Models.DTO
{
    public class UpdateWalksRequestDto
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Name has a maximum of  30 characters")]
        public string Name { get; set; }
        [Required]

        [MaxLength(1000, ErrorMessage = "Descrtiption has a maximum of  1000 characters")]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public long LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
