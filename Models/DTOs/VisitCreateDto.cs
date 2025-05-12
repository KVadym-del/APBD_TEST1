namespace APBD_TEST1.Models.DTOs
{
    public class VisitCreateDto
    {
        public int VisitId { get; set; }
        public int ClientId { get; set; }
        public string MechanicLicenceNumber { get; set; } = null!;
        public List<VisitCreateServiceDto> Services { get; set; } = new();
    }

    public class VisitCreateServiceDto
    {
        public string ServiceName { get; set; } = null!;
        public decimal ServiceFee { get; set; }
    }
}