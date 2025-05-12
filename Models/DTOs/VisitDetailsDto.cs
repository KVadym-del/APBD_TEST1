namespace APBD_TEST1.Models.DTOs
{
    public class VisitDetailsDto
    {
        public DateTime Date { get; set; }
        public ClientDto Client { get; set; } = null!;
        public MechanicDto Mechanic { get; set; } = null!;
        public List<VisitServiceDto> VisitServices { get; set; } = new();
    }

    public class ClientDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
    }

    public class MechanicDto
    {
        public int MechanicId { get; set; }
        public string LicenceNumber { get; set; } = null!;
    }

    public class VisitServiceDto
    {
        public string Name { get; set; } = null!;
        public decimal ServiceFee { get; set; }
    }
}