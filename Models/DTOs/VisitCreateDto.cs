using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APBD_TEST1.Models.DTOs
{
    public class VisitCreateDto
    {
        [Required]
        public int VisitId { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public string MechanicLicenceNumber { get; set; } = null!;

        [Required]
        [MinLength(1, ErrorMessage = "At least one service must be provided.")]
        public List<VisitCreateServiceDto> Services { get; set; } = new();
    }

    public class VisitCreateServiceDto
    {
        [Required]
        public string ServiceName { get; set; } = null!;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Service fee must be greater than 0.")]
        public decimal ServiceFee { get; set; }
    }
}