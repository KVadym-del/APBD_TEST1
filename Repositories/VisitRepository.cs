using APBD_TEST1.Data;
using APBD_TEST1.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_TEST1.Repositories
{
    public class VisitRepository : IVisitRepository
    {
        private readonly WorkshopContext _context;

        public VisitRepository(WorkshopContext context)
        {
            _context = context;
        }

        public async Task<Visit?> GetVisitWithDetailsAsync(int visitId)
        {
            return await _context.Visits
                .Include(v => v.Client)
                .Include(v => v.Mechanic)
                .Include(v => v.VisitServices)
                    .ThenInclude(vs => vs.Service)
                .FirstOrDefaultAsync(v => v.VisitId == visitId);
        }

        public async Task<bool> VisitExistsAsync(int visitId) => await _context.Visits.AnyAsync(v => v.VisitId == visitId);

        public async Task<bool> ClientExistsAsync(int clientId) => await _context.Clients.AnyAsync(c => c.ClientId == clientId);

        public async Task<Mechanic?> GetMechanicByLicenceAsync(string licenceNumber) => await _context.Mechanics.FirstOrDefaultAsync(m => m.LicenceNumber == licenceNumber);

        public async Task<Service?> GetServiceByNameAsync(string serviceName) => await _context.Services.FirstOrDefaultAsync(s => s.Name == serviceName);

        public Task AddVisitAsync(Visit visit, List<(Service service, decimal fee)> services)
        {
            _context.Visits.Add(visit);
            foreach (var (service, fee) in services)
            {
                _context.VisitServices.Add(new VisitService
                {
                    Visit = visit,
                    Service = service,
                    ServiceFee = fee
                });
            }
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}