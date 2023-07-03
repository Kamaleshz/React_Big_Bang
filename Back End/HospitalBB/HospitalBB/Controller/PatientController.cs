using HospitalBB.Models;
using HospitalBB.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalBB.Controller
{
    [EnableCors("Corspolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatient _patientRepository;

        public PatientController(IPatient patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [Authorize(Roles ="Doctor,Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatient()
        {
            try
            {
                var patients = await _patientRepository.GetPatient();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving patients: {ex.Message}");
            }
        }

        [Authorize(Roles = "Doctor,Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatientById(int id)
        {
            try
            {
                var patient = await _patientRepository.GetPatientById(id);
                if (patient == null)
                {
                    return NotFound();
                }
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving patient: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
        {
            var createdPatient = await _patientRepository.CreatePatient(patient);

            if (createdPatient == null) 
            {
                return Problem("Failed to create patient.");
            }
            return Created("Get", createdPatient);
        }

        [Authorize(Roles ="Patient,Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdatePatient(int id, Patient patient)
        {
            try
            {
                if (id != patient.PatId)
                {
                    return BadRequest("Invalid patient ID.");
                }

                var result = await _patientRepository.UpdatePatient(id, patient);
                if (result == 0)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating patient: {ex.Message}");
            }
        }
        [Authorize(Roles ="Patient,Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeletePatient(int id)
        {
            try
            {
                var result = await _patientRepository.DeletePatient(id);
                if (result == 0)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting patient: {ex.Message}");
            }
        }
    }
}
