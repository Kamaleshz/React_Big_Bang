using HospitalBB.Models;
using HospitalBB.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace HospitalBB.Repo
{
    public interface IDoctor
    {
        public IEnumerable<Doctor> GetDoctors();

        public Doctor GetDoctorById(int DocId);

        Task<Doctor> CreateDoctor([FromForm] Doctor doctor, IFormFile imageFile);

        Task<Doctor> UpdateDoctor(int DocId, Doctor doctor, IFormFile imageFile);

        public Task<UpdateStatus> UpdateStatus(UpdateStatus status);
        public Task<UpdateStatus> DeclineDoctorStatus(UpdateStatus status);

        public Task<ICollection<Doctor>> RequestedDoctor();
        public Task<ICollection<Doctor>> AcceptedDoctor();

        public Doctor DeleteDoctor(int DocId);
    }
}
