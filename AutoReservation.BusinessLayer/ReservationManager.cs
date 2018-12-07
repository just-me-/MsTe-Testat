using System.Collections.Generic;
using System.Linq;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public class ReservationManager : ManagerBase
    {
        public List<Reservation> List
        {
            get
            {
                //  return context.Reservations.ToList();
                // Reservationen oder Reservations.....??
                return UsingContext(context => includeReservationReferences(context.Reservations).ToList());
            }
        }
        public Reservation GetReservationById(int reservationsNr)
        {
            return UsingContext(context => includeReservationReferences(context.Reservations)
                                 .FirstOrDefault(reservation => reservation.ReservationsNr == reservationsNr)
                               );
        }
        public Reservation InsertReservation(Reservation reservation)
        {
            return updateReservation(reservation, EntityState.Added);
        }
        public Reservation UpdateReservation(Reservation reservation)
        {
            return updateReservation(reservation, EntityState.Modified);
        }
        public void DeleteReservation(Reservation reservation)
        {
            updateReservation(reservation, EntityState.Deleted);
        }

        private static IQueryable<Reservation> includeReservationReferences(IQueryable<Reservation> reservations)
        {
            return reservations
                    .Include(reservation => reservation.Kunde)
                    .Include(reservation => reservation.Auto);
        }
        private static Reservation updateReservation(Reservation reservation, EntityState state)
        {
            // 2DO 3.3.2 Eine Reservation muss 24 Stunden oder mehr dauern. 
            // Prüfen Sie dies und stellen Sie auch sicher, dass das «bis» Datum nicht nach dem «von» Datum liegt.
            // Falls nicht muss eine InvalidDateRangeException ausgelöst werden (noch nicht implementiert).

            // 2DO 3.3.3 Beim Erstellen / Updaten einer Reservation muss geprüft werden,
            // ob ein Auto für den gewünschten Zeitraum zur Verfügung steht. Autos können nahtlos(Ende = Star
            // t), aber nicht überlappend gebucht werden.Falls ein Auto nicht verfügbar ist,
            // muss eine AutoUnavailableException ausgelöst werden(noch nicht implementiert).

            return UsingContext(context =>
            {
                var entry = context.Entry(reservation);
                entry.State = state;
                SaveChanges(context, reservation);

                if (entry.State != EntityState.Detached)
                {
                    entry.Reference(r => r.Auto).Load();
                    entry.Reference(r => r.Kunde).Load();
                }

                return reservation;
            });
        }


    }
}