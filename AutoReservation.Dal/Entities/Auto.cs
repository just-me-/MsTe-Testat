
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoReservation.Dal.Entities
{
    
    public abstract class Auto
    {
        [Key]
        int Id { get; set; }
        [Required]
        string Marke { get; set; }
        [Timestamp]
        byte[] Timestamp { get; set; }
        [Required]
        int Tagestarif { get; set; }

        ICollection<Reservation> Reservationen { get; set; }
    }


    public class StandardAuto : Auto { }

    public class MittelklasseAuto : Auto { }

    public class LuxusAuto : Auto
    {
        [Required]
        int Basistarif { get; set; }
    }
}
