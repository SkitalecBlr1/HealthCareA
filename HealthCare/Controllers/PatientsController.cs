using HealthCare.DTOs;
using HealthCare.Services;
using HealthCare.Swagger;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace HealthCare.Controllers
{
    /// <summary>
    /// Управление пациентами
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _service;

        public PatientsController(IPatientService service)
        {
            _service = service;
        }

        /// <summary>
        /// Получить список пациентов с пагинацией
        /// </summary>
        [HttpGet]
        [SwaggerRequestExample(typeof(PatientQuery), typeof(PatientQueryExample))]
        public async Task<IActionResult> Get([FromQuery] PatientQuery query)
        {
            var (data, total) = await _service.GetPagedAsync(query);

            return Ok(new
            {
                total,
                query.Page,
                query.PageSize,
                data
            });
        }

        /// <summary>
        /// Получить пациента по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var patient = await _service.GetByIdAsync(id);
            return patient == null ? NotFound() : Ok(patient);
        }

        /// <summary>
        /// Создать пациента
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        /// <summary>
        /// Обновить пациента
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdatePatientDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Удалить пациента
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
