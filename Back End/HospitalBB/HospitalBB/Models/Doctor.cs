using System.ComponentModel.DataAnnotations;

namespace HospitalBB.Models
{
    public class Doctor
    {
        [Key]
        public int DocId { get; set; }

        public string? DocName { get; set; }

        public string? DocEmail { get; set; }

        public string? DocPassword { get; set; }

        public string? Speciality { get; set; }

        public int DocAge { get; set; }

        public string? DocDescription { get; set; }

        public int Experience { get; set; }

        public int DocPhoneNumber { get; set; }

        public string? Status { get; set; }

        public ICollection<Patient>? Patients { get; set; }

    }
}
