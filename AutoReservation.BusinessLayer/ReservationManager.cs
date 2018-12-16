using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;


namespace AutoReservation.BusinessLayer
{
    public class ReservationManager : ManagerBase
    {

        public static readonly string vonNachBisMessage = "Zeitreisen oder was?";
        public static readonly string min24hMessage = "Sie müssen mindestens 24h reservieren";
        public static readonly string alreadyReserved = "Der Karren ist schon reserviert";

        public static List<Reservation> List
        {
            get
            {
                //  return context.Reservations.ToList();
                // Reservationen oder Reservations.....??
                return UsingContext(context => includeReservationReferences(context.Reservations).ToList());
            }
        }
        public static Reservation GetReservationById(int reservationsNr)
        {    
            return UsingContext(context => includeReservationReferences(context.Reservations)
                                 .FirstOrDefault(reservation => reservation.ReservationsNr == reservationsNr)
                               );
        }
        public static Reservation InsertReservation(Reservation reservation)
        {
            checkForAvailabilityException(reservation);
            checkForDateRangeException(reservation);
            return updateReservation(reservation, EntityState.Added);
        }
        public static Reservation UpdateReservation(Reservation reservation)
        {
            checkForAvailabilityException(reservation);
            checkForDateRangeException(reservation);
            return updateReservation(reservation, EntityState.Modified);
        }
        public static void DeleteReservation(Reservation reservation)
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
            if (state != EntityState.Deleted)
            {
                checkForAvailabilityException(reservation);
                checkForDateRangeException(reservation);
            }

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

        private static void checkForDateRangeException(Reservation res)
        {
            DateTime von = res.Von;
            DateTime bis = res.Bis;
            DateTime oneDayLater = von.AddDays(1);

            //von datum muss mindestens 24h vor bis datum sein
            //es wird implizit gleichzeitig geprüft, dass (von < bis)
            if (oneDayLater > bis)
            {
                InvalidDateRangeFault fault = new InvalidDateRangeFault();
                if (bis < von)
                {
                    fault.Message = vonNachBisMessage;
                }
                else
                {
                    fault.Message = min24hMessage;
                }

                fault.Bis = bis;
                fault.Von = von;
                throw new FaultException<InvalidDateRangeFault>(fault);
            }

        }

        private static void checkForAvailabilityException(Reservation res)
        {

            DateTime wantedStart = res.Von;
            DateTime wantedEnd = res.Bis;
            int myAuto = res.AutoId;

            List<Reservation> reservations = List;

            var reservedDates =
                from r in reservations
                where r.AutoId == myAuto
                select new
                {
                    r.Von,
                    r.Bis
                };

            foreach (var reservedDate in reservedDates)
            {
                //ich reserviere, nachdem es jeman anders reserviert hat &&
                //der andere hat es noch nicht wieder zurückgegeben
                if ( Overlap(reservedDate.Von, reservedDate.Bis, wantedStart, wantedEnd) )
                {
                    AutoUnavailableFault fault = new AutoUnavailableFault();
                    fault.Message = alreadyReserved;
                    fault.Bis = wantedEnd;
                    fault.Von = wantedStart;
                    throw new FaultException<AutoUnavailableFault>(fault);
                }

            }
        }

        private static bool Overlap (DateTime aStart, DateTime aEnd, DateTime bStart, DateTime bEnd)
        {

            return ! (aEnd <= bStart || aStart >= bEnd );
           
        }


    }
}