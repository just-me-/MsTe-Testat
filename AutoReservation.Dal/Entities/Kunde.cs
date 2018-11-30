using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    [Table("Kunden", Schema = "dbo")]
    public class Kunde
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(500)]
        public string Vorname { get; set; }
        [Required, MaxLength(500)]
        public string Nachname { get; set; }
        [Required]
        public DateTime Geburtsdatum { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }

        public ICollection<Reservation> Reservationen { get; set; }
    }

}
