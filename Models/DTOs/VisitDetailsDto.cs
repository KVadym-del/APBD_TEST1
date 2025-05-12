using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APBD_TEST1.Models.DTOs
{
    public class VisitDetailsDto
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public ClientDto Client { get; set; } = null!;
        [Required]
        public MechanicDto Mechanic { get; set; } = null!;
        [Required]
        [MinLength(1, ErrorMessage = "At least one service must be provided.")]
        public List<VisitServiceDto> VisitServices { get; set; } = new();
    }

    public class ClientDto
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public DateTime DateOfBirth { get; set; }
    }

    public class MechanicDto
    {
        [Required]
        public int MechanicId { get; set; }
        [Required]
        public string LicenceNumber { get; set; } = null!;
    }

    public class VisitServiceDto
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Service fee must be greater than 0.")]
        public decimal ServiceFee { get; set; }
    }
}