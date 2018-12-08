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
        AutoDto GetKundeDtoById(int Id);

        [OperationContract]
        void InsertKunde(AutoDto autoDto);

        [OperationContract]
        void UpdateKunde(AutoDto autoDto);

        [OperationContract]
        void DeleteKunde(AutoDto autoDto);

        // Reservation
        [OperationContract]
        List<ReservationDto> GetAllReservationDtos();

        [OperationContract]
        AutoDto GetReservationDtoById(int Id);

        [OperationContract]
        [FaultContract(typeof(CRUDException))]
        void InsertReservation(AutoDto autoDto);

        [OperationContract]
        void UpdateReservation(AutoDto autoDto);

        [OperationContract]
        void DeleteReservation(AutoDto autoDto);
    }
}
