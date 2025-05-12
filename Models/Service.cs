namespace APBD_TEST1.Models
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string Name { get; set; } = null!;
        public decimal BaseFee { get; set; }
        public ICollection<VisitService> VisitServices { get; set; } = new List<VisitService>();
    }
}