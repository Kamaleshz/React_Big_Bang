using HospitalBB.Models;

namespace HospitalBB.Repo
{
    public interface IPatient
    {
        Task<IEnumerable<Patient>> GetPatient();

        Task<Patient> GetPatientById(int id);

        Task<Patient> CreatePatient(Patient patient);

        Task<int> UpdatePatient(int id, Patient patient);

        Task<int> DeletePatient(int PatId);
    }
}
