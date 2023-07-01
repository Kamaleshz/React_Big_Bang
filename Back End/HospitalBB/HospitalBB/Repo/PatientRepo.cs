using HospitalBB.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalBB.Repo
{
    public class PatientRepo:IPatient
    {
        HospitalBBContextClass _dbContext;
        
        public PatientRepo(HospitalBBContextClass dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Patient>> GetPatient()
        {
            try
            {
                return await _dbContext.Patients.Include(x => x.Doctors).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve patients.", ex);
            }
        }
        public async Task<Patient> GetPatientById(int PatId)
        {
            try
            {
                return await _dbContext.Patients.FirstOrDefaultAsync(p => p.PatId == PatId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve the patient by ID.", ex);
            }
        }
        public async Task<Patient> CreatePatient(Patient patient)
        {
            try
            {
                if (_dbContext.Patients == null)
                {
                    throw new NullReferenceException("Entity set 'HospitalContext.patients' is null.");
                }

                _dbContext.Patients.Add(patient);
                await _dbContext.SaveChangesAsync();

                return patient;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<int> UpdatePatient(int id, Patient patient)
        {
            try
            {

                _dbContext.Entry(patient).State = EntityState.Modified;
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update the patient.", ex);
            }
        }
        public async Task<int> DeletePatient(int patientId)
        {
            try
            {
                var patient = await _dbContext.Patients.FindAsync(patientId);
                if (patient != null)
                {
                    _dbContext.Patients.Remove(patient);
                    return await _dbContext.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete the patient.", ex);
            }
        }
    }
}
