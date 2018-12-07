
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoReservation.Dal.Entities
{
    
    public abstract class Auto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Marke { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
        [Required]
        public int Tagestarif { get; set; }

        public ICollection<Reservation> Reservationen { get; set; }
    }


    public class StandardAuto : Auto { }

    public class MittelklasseAuto : Auto { }

    public class LuxusklasseAuto : Auto
    {
        [Required]
        public int Basistarif { get; set; }
    }
}
