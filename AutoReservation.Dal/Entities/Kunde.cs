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
        int Id { get; set; }
        [Required, MaxLength(500)]
        string Vorname { get; set; }
        [Required, MaxLength(500)]
        string Nachname { get; set; }
        [Required]
        DateTime Geburtsdatum { get; set; }
        [Timestamp]
        byte[] Timestamp { get; set; }

        ICollection<Reservation> Reservationen { get; set; }
    }

}
