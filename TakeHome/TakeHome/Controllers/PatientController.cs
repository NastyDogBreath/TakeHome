using Microsoft.AspNetCore.Mvc;
using TakeHome.Models;
using TakeHome.Service;

namespace TakeHome.API.Controllers
{
    [ApiController]
    [Route("patients")]
    public class PatientController : Controller
    {
        private readonly IPatientService _service;
        public PatientController(IPatientService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody] PatientModel patient)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var created = _service.Create(patient);
            // 201 response for created resource
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var patient = _service.GetById(id);
            if (patient == null)
                return NotFound(new { message = "Patient not found" });
            return Ok(patient);
        }

        [HttpGet]
        
        public IActionResult Search([FromQuery] string? firstname, [FromQuery] string? lastName, [FromQuery] DateTime? dateOfBirth)
        {
            var results = _service.Search(firstname, lastName, dateOfBirth);
            return Ok(results);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PatientModel updatedPatient)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            _service.Update(id, updatedPatient);
            return NoContent();
        }
    }
}
