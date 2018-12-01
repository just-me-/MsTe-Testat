using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    [Table("Reservationen", Schema = "dbo")]
    public class Reservation
    {
        [Key]
        public int ReservationsNr { get; set; }
        [Required]
        public int AutoId { get; set; }
        [Required]
        public int KundeId { get; set; }
        [Required]
        public DateTime Von { get; set; }
        [Required]
        public DateTime Bis { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
        [ForeignKey(nameof(AutoId))]
        public Auto Auto { get; set; }
        [ForeignKey(nameof(KundeId))]
        public Kunde Kunde { get; set; }
    }

}
