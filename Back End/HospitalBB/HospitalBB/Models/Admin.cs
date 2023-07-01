using System.ComponentModel.DataAnnotations;

namespace HospitalBB.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        public string? AdminName { get; set; }

        public string? AdminEmail { get; set; }

        public string? AdminPassword { get; set; }

<<<<<<< HEAD
=======
        dfkj
>>>>>>> 35cd45aea68e3b3b0e1d8d2f4bf13e5a09c45758
    }
}
