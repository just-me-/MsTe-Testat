using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Common.FaultExceptions;
using System.Collections.Generic;
using System.ServiceModel;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {
        // Auto
        [OperationContract]
        List<AutoDto> GetAllAutoDtos();

        [OperationContract]
        AutoDto GetAutoDtoById(int Id);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault))]
        AutoDto InsertAuto(AutoDto autoDto);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault))]
        void UpdateAuto(AutoDto autoDto);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault))]
        void DeleteAuto(AutoDto autoDto);

        // Kunde
        [OperationContract]
        List<KundeDto> GetAllKundenDtos();

        [OperationContract]
        KundeDto GetKundeDtoById(int Id);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault))]
        void InsertKunde(KundeDto kundeDto);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault))]
        void UpdateKunde(KundeDto kundeDto);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault))]
        void DeleteKunde(KundeDto kundeDto);

        // Reservation
        [OperationContract]
        List<ReservationDto> GetAllReservationDtos();

        [OperationContract]
        ReservationDto GetReservationDtoById(int Id);

        [OperationContract]
        [FaultContract(typeof(AutoUnavailableFault))]
        [FaultContract(typeof(InvalidDateRangeFault))]
        [FaultContract(typeof(OptimisticConcurrencyFault))]
        void InsertReservation(ReservationDto reservationDto);

        [OperationContract]
        [FaultContract(typeof(AutoUnavailableFault))]
        [FaultContract(typeof(InvalidDateRangeFault))]
        [FaultContract(typeof(OptimisticConcurrencyFault))]
        void UpdateReservation(ReservationDto reservationDto);

        [OperationContract]
        [FaultContract(typeof(OptimisticConcurrencyFault))]
        void DeleteReservation(ReservationDto reservationDto);
    }
}
