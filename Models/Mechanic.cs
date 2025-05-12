namespace APBD_TEST1.Models
{
    public class Mechanic
    {
        public int MechanicId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string LicenceNumber { get; set; } = null!;
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
    }
}