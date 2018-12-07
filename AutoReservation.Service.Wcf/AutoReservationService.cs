using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {

        private static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");

        public void DeleteAuto(AutoDto autoDto)
        {
            
        }

        public void DeleteKunde(AutoDto autoDto)
        {
            throw new NotImplementedException();
        }

        public void DeleteReservation(AutoDto autoDto)
        {
            throw new NotImplementedException();
        }

        public List<AutoDto> GetAllAutoDtos()
        {
            throw new NotImplementedException();
        }

        public List<KundeDto> GetAllKundenDtos()
        {
            throw new NotImplementedException();
        }

        public List<ReservationDto> GetAllReservationDtos()
        {
            throw new NotImplementedException();
        }

        public AutoDto GetAutoDtoById(int Id)
        {
            throw new NotImplementedException();
        }

        public AutoDto GetKundeDtoById(int Id)
        {
            throw new NotImplementedException();
        }

        public AutoDto GetReservationDtoById(int Id)
        {
            throw new NotImplementedException();
        }

        public void InsertAuto(AutoDto autoDto)
        {
            throw new NotImplementedException();
        }

        public void InsertKunde(AutoDto autoDto)
        {
            throw new NotImplementedException();
        }

        public void InsertReservation(AutoDto autoDto)
        {
            throw new NotImplementedException();
        }

        public void UpdateAuto(AutoDto autoDto)
        {
            throw new NotImplementedException();
        }

        public void UpdateKunde(AutoDto autoDto)
        {
            throw new NotImplementedException();
        }

        public void UpdateReservation(AutoDto autoDto)
        {
            throw new NotImplementedException();
        }
    }
}