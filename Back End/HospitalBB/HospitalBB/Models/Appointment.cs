using System.ComponentModel.DataAnnotations;

namespace HospitalBB.Models
{
    public class Appointment
    {
        [Key]
        public int AppId { get; set; }

        public string? Date { get; set; }

        public int PatientId  { get; set; }

        public int DocId { get; set; }

        public bool IsConfirmed { get; set; }

        public Doctor? Doctor { get; set; }

        public Patient? Patient { get; set; }

        public ICollection<Patient>? Patients { get; set; }
    }
}
