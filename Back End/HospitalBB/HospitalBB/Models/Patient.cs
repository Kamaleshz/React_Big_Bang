using System.ComponentModel.DataAnnotations;

namespace HospitalBB.Models
{
    public class Patient
    {
        [Key]

        public int PatID { get; set; }

        public string? PatName { get; set; }

        public string? PatEmail { get; set; }

        public string? PatPassword { get; set; }

        public int PatPhoneNumber { get; set;}

        public string? PatGender { get; set; }

        public int? Age { get; set; }

        public Doctor? Doctors { get; set; }


    }
}
