using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalBB.Models
{
    public class Patient
    {
        [Key]

        public int PatId { get; set; }

        [ForeignKey("DocId")]
        public int DocId { get; set; }

        public string? PatName { get; set; }

        public string? PatEmail { get; set; }

        public string? PatPassword { get; set; }

        public int PatPhoneNumber { get; set;}

        public string? PatGender { get; set; }

        public int? Age { get; set; }

        public string? PatIssue { get; set; }

        public Doctor? Doctors { get; set; }


    }
}
