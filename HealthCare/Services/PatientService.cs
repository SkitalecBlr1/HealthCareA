using AutoMapper;
using HealthCare.DTOs;
using HealthCare.Entities;
using HealthCare.Repositories;

namespace HealthCare.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientService> _logger;

        public PatientService(IPatientRepository repo, IMapper mapper, ILogger<PatientService> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(List<PatientDto>, int)> GetPagedAsync(PatientQuery query)
        {
            var (entities, total) = await _repo.GetPagedAsync(query);
            return (_mapper.Map<List<PatientDto>>(entities), total);
        }

        public async Task<PatientDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<PatientDto>(entity);
        }

        public async Task<PatientDto> CreateAsync(CreatePatientDto dto)
        {
            var entity = _mapper.Map<Patient>(dto);
            entity.Id = Guid.NewGuid();

            _logger.LogInformation("Creating patient {@Patient}", entity);

            await _repo.AddAsync(entity);

            return _mapper.Map<PatientDto>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdatePatientDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            _mapper.Map(dto, existing);

            await _repo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
