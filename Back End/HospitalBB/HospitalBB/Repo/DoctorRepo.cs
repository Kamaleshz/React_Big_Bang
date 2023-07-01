using HospitalBB.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalBB.Repo
{
    public class DoctorRepo : IDoctor
    {
        private readonly HospitalBBContextClass _hospContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DoctorRepo(HospitalBBContextClass con, IWebHostEnvironment webHostEnvironment)
        {
            _hospContext = con;
            _webHostEnvironment = webHostEnvironment;
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            return _hospContext.Doctors.Include(x => x.Patients).ToList();
        }

        public Doctor GetDoctorById(int DocId)
        {
            try
            {
                return _hospContext.Doctors.FirstOrDefault(x => x.DocId == DocId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Doctor> CreateDoctor([FromForm] Doctor doctor, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            doctor.Status = "Not Admitted";

            doctor.DocImg = fileName;


            _hospContext.Doctors.Add(doctor);
            await _hospContext.SaveChangesAsync();

            return doctor;
        }
        public async Task<Doctor> UpdateDoctor(int DocId, Doctor doctor, IFormFile imageFile)
        {
            var existingDoctor = await _hospContext.Doctors.FindAsync(DocId);
            if (existingDoctor == null)
            {
                return null;
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                var oldFilePath = Path.Combine(uploadsFolder, existingDoctor.DocImg);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }

                existingDoctor.DocImg = fileName;
            }

            existingDoctor.DocName = doctor.DocName;
            existingDoctor.Speciality = doctor.Speciality;
            existingDoctor.DocGender = doctor.DocGender;
            existingDoctor.Experience = doctor.Experience;
            existingDoctor.DocEmail= doctor.DocEmail;
            existingDoctor.DocPassword = doctor.DocPassword;
            existingDoctor.DocAge = doctor.DocAge;
            existingDoctor.DocDescription = doctor.DocDescription;
            existingDoctor.DocPhoneNumber = doctor.DocPhoneNumber;
            await _hospContext.SaveChangesAsync();

            return existingDoctor;
        }
        public Doctor DeleteDoctor(int DocId)
        {
            try
            {
                Doctor doctor = _hospContext.Doctors.FirstOrDefault(x => x.DocId == DocId);
                if (doctor != null)
                {
                    _hospContext.Doctors.Remove(doctor);
                    _hospContext.SaveChanges();
                    return doctor;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
