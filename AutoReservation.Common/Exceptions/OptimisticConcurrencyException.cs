using System.Runtime.Serialization;
using AutoReservation.BusinessLayer;

namespace AutoReservation.Common.Exceptions
{
    [DataContract]
    public class OptimisticConcurrencyException<T> where T : ManagerBase
    {
        [DataMember]
        public T Entity { get; set; }
    }
}