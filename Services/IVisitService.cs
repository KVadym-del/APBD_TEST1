using APBD_TEST1.Models.DTOs;

namespace APBD_TEST1.Services
{
    public interface IVisitService
    {
        Task<VisitDetailsDto?> GetVisitDetailsAsync(int visitId);
        Task<(bool Success, string? Error)> AddVisitAsync(VisitCreateDto dto);
    }
}