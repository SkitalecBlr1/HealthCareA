using HealthCare.DTOs;
using HealthCare.Entities;

namespace HealthCare.Repositories
{
    public interface IPatientRepository
    {
        Task<(List<Patient>, int)> GetPagedAsync(PatientQuery query);
        Task<Patient?> GetByIdAsync(Guid id);
        Task AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task DeleteAsync(Guid id);
    }
}
