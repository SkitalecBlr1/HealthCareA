using HealthCare.DTOs;

namespace HealthCare.Services
{
    public interface IPatientService
    {
        Task<(List<PatientDto>, int)> GetPagedAsync(PatientQuery query);
        Task<PatientDto?> GetByIdAsync(Guid id);
        Task<PatientDto> CreateAsync(CreatePatientDto dto);
        Task<bool> UpdateAsync(Guid id, UpdatePatientDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
