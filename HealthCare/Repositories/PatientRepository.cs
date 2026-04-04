using HealthCare.Database;
using HealthCare.DTOs;
using HealthCare.Entities;
using HealthCare.Specifications;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(List<Patient>, int)> GetPagedAsync(PatientQuery query)
        {
            var spec = PatientSpecificationBuilder.Build(query);

            var q = _context.Patients
                .Where(spec.Criteria);

            var total = await q.CountAsync();

            var data = await q
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return (data, total);
        }

        public async Task<Patient?> GetByIdAsync(Guid id)
            => await _context.Patients.FindAsync(id);

        public async Task AddAsync(Patient patient)
        {
            await _context.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Patient patient)
        {
            _context.Update(patient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Patients.FindAsync(id);
            if (entity == null) return;

            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
