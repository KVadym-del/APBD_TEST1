using APBD_TEST1.Models.DTOs;
using APBD_TEST1.Repositories;
using APBD_TEST1.Models;

namespace APBD_TEST1.Services
{
    public class VisitService : IVisitService
    {
        private readonly IVisitRepository _repo;

        public VisitService(IVisitRepository repo)
        {
            _repo = repo;
        }

        public async Task<VisitDetailsDto?> GetVisitDetailsAsync(int visitId)
        {
            var visit = await _repo.GetVisitWithDetailsAsync(visitId);
            if (visit == null) return null;

            return new VisitDetailsDto
            {
                Date = visit.Date,
                Client = new ClientDto
                {
                    FirstName = visit.Client.FirstName,
                    LastName = visit.Client.LastName,
                    DateOfBirth = visit.Client.DateOfBirth
                },
                Mechanic = new MechanicDto
                {
                    MechanicId = visit.Mechanic.MechanicId,
                    LicenceNumber = visit.Mechanic.LicenceNumber
                },
                VisitServices = visit.VisitServices.Select(vs => new VisitServiceDto
                {
                    Name = vs.Service.Name,
                    ServiceFee = vs.ServiceFee
                }).ToList()
            };
        }

        public async Task<(bool Success, string? Error)> AddVisitAsync(VisitCreateDto dto)
        {
            if (dto.VisitId <= 0)
                return (false, "visitId must be a positive, non-zero integer.");

            if (await _repo.VisitExistsAsync(dto.VisitId))
                return (false, "Visit with the provided ID already exists.");

            if (!await _repo.ClientExistsAsync(dto.ClientId))
                return (false, "Client with the provided ID does not exist.");

            var mechanic = await _repo.GetMechanicByLicenceAsync(dto.MechanicLicenceNumber);
            if (mechanic == null)
                return (false, "Mechanic with the provided licence number does not exist.");

            var services = new List<(Service, decimal)>();
            foreach (var s in dto.Services)
            {
                var service = await _repo.GetServiceByNameAsync(s.ServiceName);
                if (service == null)
                    return (false, $"Service with name '{s.ServiceName}' does not exist.");
                services.Add((service, s.ServiceFee));
            }

            if (!services.Any())
                return (false, "At least one service must be provided.");

            var visit = new Visit
            {
                VisitId = dto.VisitId,
                ClientId = dto.ClientId,
                MechanicId = mechanic.MechanicId,
                Date = DateTime.Now
            };

            await _repo.AddVisitAsync(visit, services);
            await _repo.SaveChangesAsync();
            return (true, null);
        }
    }
}