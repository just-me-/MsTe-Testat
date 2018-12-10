using System.Runtime.Serialization;

namespace AutoReservation.Common.Exceptions
{
    [DataContract]
    public class OptimisticConcurrencyException<T>
    {
        [DataMember]
        public T Entity { get; set; }
    }
}