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

        private static TEntity handlingOptimisticConcurrencyException<TEntity>(string operation, Func<TEntity> func)
        {
            try
            {
                return func();
            }
            catch (LocalOptimisticConcurrencyException<TEntity> e)
            {
                var fault = new OptimisticConcurrencyFault()
                {

                    Message = "Concurrency error during " + operation
                };
                throw new FaultException<OptimisticConcurrencyFault>(fault);
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

        public AutoDto InsertAuto(AutoDto autoDto)
        {
            WriteActualMethod();

            return AutoManager.InsertAuto(autoDto.ConvertToEntity()).ConvertToDto();    //hier convert to dto und nicht void. //***!!!*///
        }

        public KundeDto InsertKunde(KundeDto kundeDto)
        {
            WriteActualMethod();
            return KundeManager.InsertKunde(kundeDto.ConvertToEntity()).ConvertToDto(); ;
        }

        public ReservationDto InsertReservation(ReservationDto reservationDto)
        {
            WriteActualMethod();

            return ReservationManager.InsertReservation(reservationDto.ConvertToEntity()).ConvertToDto();
        }

        public AutoDto UpdateAuto(AutoDto autoDto)
        {
            WriteActualMethod();
            return handlingOptimisticConcurrencyException<AutoDto>("UpdateAuto",
                () => AutoManager.UpdateAuto(autoDto.ConvertToEntity()).ConvertToDto()
                );
        }

        public KundeDto UpdateKunde(KundeDto kundeDto)
        {
            WriteActualMethod();
            return handlingOptimisticConcurrencyException<KundeDto>("UpdateKunde",
                () => KundeManager.UpdateKunde(kundeDto.ConvertToEntity()).ConvertToDto()
                );
        }

        public ReservationDto UpdateReservation(ReservationDto reservationDto)
        {
            WriteActualMethod();

            return handlingOptimisticConcurrencyException<ReservationDto>("UpdateReservation",
                () => ReservationManager.UpdateReservation(reservationDto.ConvertToEntity()).ConvertToDto()
                );
        }
    }
}