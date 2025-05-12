namespace APBD_TEST1.Models
{
    public class VisitService
    {
        public int VisitId { get; set; }
        public int ServiceId { get; set; }
        public decimal ServiceFee { get; set; }

        public Visit Visit { get; set; } = null!;
        public Service Service { get; set; } = null!;
    }
}