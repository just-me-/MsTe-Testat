using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
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
        void InsertAuto(AutoDto autoDto);

        [OperationContract]
        void UpdateAuto(AutoDto autoDto);

        [OperationContract]
        void DeleteAuto(AutoDto autoDto);

        // Kunde
        [OperationContract]
        List<KundeDto> GetAllKundenDtos();

        [OperationContract]
        KundeDto GetKundeDtoById(int Id);

        [OperationContract]
        void InsertKunde(KundeDto kundeDto);

        [OperationContract]
        void UpdateKunde(KundeDto kundeDto);

        [OperationContract]
        void DeleteKunde(KundeDto kundeDto);

        // Reservation
        [OperationContract]
        List<ReservationDto> GetAllReservationDtos();

        [OperationContract]
        ReservationDto GetReservationDtoById(int Id);

        [OperationContract]
        [FaultContract(typeof(CRUDException))]
        void InsertReservation(ReservationDto reservationDto);

        [OperationContract]
        void UpdateReservation(ReservationDto reservationDto);

        [OperationContract]
        void DeleteReservation(ReservationDto reservationDto);
    }
}
