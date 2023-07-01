using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalBB.Models
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int DocId { get; set; }

        public string? DocName { get; set; }

        public string? DocEmail { get; set; }

        public string? DocPassword { get; set; }

        public string? Speciality { get; set; }

        public int DocAge { get; set; }

        public string? DocGender { get; set; }

        public string? DocDescription { get; set; }

        public int Experience { get; set; }

        public int DocPhoneNumber { get; set; }

        public string? DocImg { get; set; }

        public string? Status { get; set; }

        public ICollection<Patient>? Patients { get; set; }

    }
}
