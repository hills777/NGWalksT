using System.ComponentModel.DataAnnotations;

namespace NGWalks.Models.DTO
{
    public class UpdateRegionRquestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a minimum of  3 characters")]
        [MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Name has a maximum of  30 characters")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
