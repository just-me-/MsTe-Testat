using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.FaultExceptions;
using AutoReservation.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects.Faults;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {

        private static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");

        private static void handlingOptimisticConcurrencyException<TEntity>(string operation, Action func)
        {
            try
            {
                func();
            }
            catch (LocalOptimisticConcurrencyException<TEntity> e)
            {
                var fault = new OptimisticConcurrencyFaultContract()
                {
                    Operation = operation,
                    Message = e.Message
                };
                throw new FaultException<OptimisticConcurrencyFaultContract>(fault);
            }
        }



        private static void checkForDateRangeException(ReservationDto reservationDto)
        {
            DateTime von = reservationDto.Von;
            DateTime bis = reservationDto.Bis;
            TimeSpan oneDay = new TimeSpan(1, 0, 0, 0);
            DateTime _24hLater = von.Add(oneDay);

            //von datum muss mindestens 24h vor bis datum sein
            //es wird implizit gleichzeitig geprüft, dass (von < bis)
            if (_24hLater < bis)
            {
                InvalidDateRangeFault fault = new InvalidDateRangeFault();
                if (bis < von)
                {
                    fault.Message = "Zeitreisen oder was?";
                }
                else
                {
                    fault.Message = "Sie müssen mindestens 24h reservieren";
                }

                fault.Bis = bis;
                fault.Von = von;
                throw new FaultException<InvalidDateRangeFault>(fault);
            }

        }

        private void checkForAvailabilityException(ReservationDto reservationDto)
        {

            DateTime start = reservationDto.Von;
            DateTime end = reservationDto.Bis;
            AutoDto myAuto = reservationDto.Auto;

            List<ReservationDto> reservations = GetAllReservationDtos();

            var reservedDates =
                from r in reservations
                where r.Auto == myAuto
                select new
                {
                    r.Von,
                    r.Bis
                };

            foreach (var reservedDate in reservedDates)
            {
                //ich reserviere, nachdem es jeman anders reserviert hat &&
                //der andere hat es noch nicht wieder zurückgegeben
                if (reservedDate.Von < start && end < reservedDate.Bis)
                {
                    AutoUnavailableFault fault = new AutoUnavailableFault();
                    fault.Message = "Das Auto ist schon reserviert";
                    fault.Bis = end;
                    fault.Von = start;
                    throw new FaultException<AutoUnavailableFault>(fault);
                }

            }
        }



        public void DeleteAuto(AutoDto autoDto)
        {
            WriteActualMethod();
            AutoManager.DeleteAuto(DtoConverter.ConvertToEntity(autoDto));
        }

        public void DeleteKunde(KundeDto kundeDto)
        {
            WriteActualMethod();
            KundeManager.DeleteKunde(DtoConverter.ConvertToEntity(kundeDto));
        }

        public void DeleteReservation(ReservationDto reservationDto)
        {
            WriteActualMethod();
            ReservationManager.DeleteReservation(DtoConverter.ConvertToEntity(reservationDto));
        }

        public List<AutoDto> GetAllAutoDtos()
        {
            WriteActualMethod();
            return AutoManager.GetAllAutos().ConvertToDtos(); 
        }

        public List<KundeDto> GetAllKundenDtos()
        {
            WriteActualMethod();
            return KundeManager.List.ConvertToDtos();
        }

        public List<ReservationDto> GetAllReservationDtos()
        {
            WriteActualMethod();
            return ReservationManager.List.ConvertToDtos();
        }

        public AutoDto GetAutoDtoById(int Id)
        {
            WriteActualMethod();
            return AutoManager.GetAutoById(Id).ConvertToDto();
        }

        public KundeDto GetKundeDtoById(int Id)
        {
            WriteActualMethod();
            return KundeManager.GetKundeById(Id).ConvertToDto();
        }

        public ReservationDto GetReservationDtoById(int Id)
        {
            WriteActualMethod();
           return ReservationManager.GetReservationById(Id).ConvertToDto();
        }

        public void InsertAuto(AutoDto autoDto)
        {
            WriteActualMethod();
            AutoManager.InsertAuto(autoDto.ConvertToEntity());
        }

        public void InsertKunde(KundeDto kundeDto)
        {
            WriteActualMethod();
            KundeManager.InsertKunde(kundeDto.ConvertToEntity());
        }

        public void InsertReservation(ReservationDto reservationDto)
        {
            WriteActualMethod();

        
            checkForDateRangeException(reservationDto);
            checkForAvailabilityException(reservationDto);

            ReservationManager.InsertReservation(reservationDto.ConvertToEntity());
        }

        public void UpdateAuto(AutoDto autoDto)
        {
            WriteActualMethod();
            handlingOptimisticConcurrencyException<AutoDto>("UpdateAuto",
                () => AutoManager.UpdateAuto(autoDto.ConvertToEntity()));
        }

        public void UpdateKunde(KundeDto kundeDto)
        {
            WriteActualMethod();
            handlingOptimisticConcurrencyException<KundeDto>("UpdateKunde",
                () => KundeManager.UpdateKunde(kundeDto.ConvertToEntity()));
        }

        public void UpdateReservation(ReservationDto reservationDto)
        {
            WriteActualMethod();

            checkForDateRangeException(reservationDto);
            checkForAvailabilityException(reservationDto);

            handlingOptimisticConcurrencyException<ReservationDto>("UpdateReservation",
                () => ReservationManager.UpdateReservation(reservationDto.ConvertToEntity()));
        }
    }
}