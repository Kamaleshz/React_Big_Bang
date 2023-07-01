using HospitalBB.Models;
using HospitalBB.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalBB.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {

        private readonly IDoctor doctor;

        public DoctorController(IDoctor doctor)
        {
            this.doctor = doctor;

        }
        //[Authorize(Roles ="Doctor,Admin,Patient")]
        [HttpGet]
        public IEnumerable<Doctor>? Get()
        {

            return doctor.GetDoctors();
        }

        [Authorize(Roles = "Doctor,Admin,Patient")]
        [HttpGet("{Doctor_Id}")]
        public Doctor? Doctor_Id(int Doctor_Id)
        {

            return doctor.GetDoctorById(Doctor_Id);


        }

        [HttpPost]
        public async Task<ActionResult<Doctor>> Post([FromForm] Doctor doc, IFormFile imageFile)
        {

            try
            {
                var createdCourse = await doctor.CreateDoctor(doc, imageFile);
                return Created("Get", createdCourse);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        //[Authorize(Roles = "Doctor,Admin")]
        [HttpPut("{DocId}")]
        public async Task<ActionResult<Doctor>> Put(int DocId, [FromForm] Doctor doc, IFormFile imageFile)
        {
            try
            {
                var updatedCake = await doctor.UpdateDoctor(DocId, doc, imageFile);
                if (updatedCake == null)
                {
                    return NotFound();
                }

                return Ok(updatedCake);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        //[Authorize(Roles = "Doctor,Admin")]
        [HttpDelete("{Doctor_Id}")]
        public Doctor? DeleteCake(int Doctor_Id)
        {
            return doctor.DeleteDoctor(Doctor_Id);
        }
    }
}
