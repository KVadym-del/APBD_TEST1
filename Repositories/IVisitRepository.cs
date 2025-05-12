using APBD_TEST1.Models;

namespace APBD_TEST1.Repositories
{
    public interface IVisitRepository
    {
        Task<Visit?> GetVisitWithDetailsAsync(int visitId);
        Task<bool> VisitExistsAsync(int visitId);
        Task<bool> ClientExistsAsync(int clientId);
        Task<Mechanic?> GetMechanicByLicenceAsync(string licenceNumber);
        Task<Service?> GetServiceByNameAsync(string serviceName);
        Task AddVisitAsync(Visit visit, List<(Service service, decimal fee)> services);
        Task SaveChangesAsync();
    }
}