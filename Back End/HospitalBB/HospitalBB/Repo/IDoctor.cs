using HospitalBB.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalBB.Repo
{
    public interface IDoctor
    {
        public IEnumerable<Doctor> GetDoctors();

        public Doctor GetDoctorById(int DocId);

        Task<Doctor> CreateDoctor([FromForm] Doctor doctor, IFormFile imageFile);

        Task<Doctor> UpdateDoctor(int DocId, Doctor doctor, IFormFile imageFile);

        public Doctor DeleteDoctor(int DocId);
    }
}
