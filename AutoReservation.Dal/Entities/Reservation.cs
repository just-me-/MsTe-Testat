using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    [Table("Reservationen", Schema = "dbo")]
    public class Reservation
    {
        [Key]
        int ReservationsNr { get; set; }
        [Required]
        int AutoId { get; set; }
        [Required]
        int KundeId { get; set; }
        [Required]
        DateTime Von { get; set; }
        [Required]
        DateTime Bis { get; set; }
        [Timestamp]
        byte[] Timestamp { get; set; }
        [ForeignKey(nameof(AutoId))]
        public Auto Auto { get; set; }
        [ForeignKey(nameof(KundeId))]
        public Kunde Kunde { get; set; }
    }

}
