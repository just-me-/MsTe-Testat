using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using PropertyChanged;

namespace AutoReservation.Common.DataTransferObjects
{
    
    
    [DataContract]
    [AddINotifyPropertyChangedInterface]
    public class AutoDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Marke { get; set; }
        [DataMember]
        public byte[] Timestamp { get; set; }
        [DataMember]
        public int Basistarif { get; set; }
        [DataMember]
        public int Tagestarif { get; set; }
        [DataMember]
        public AutoKlasse AutoKlasse { get; set; }

        //public ICollection<ReservationDto> Reservationen { get; set; }

        //public override string ToString()
        //    => $"{Id}; {Marke}; {Tagestarif}; {Basistarif}; {AutoKlasse}; {Timestamp}";


        /*
        //For INotiofyPropertyChanged
        //usage: set {SetPreoperty(ref_field, vlaue, nameof(Property)
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string name = null,
            params string[] otherNames)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(name);
            foreach (var n in otherNames)
            {
                OnPropertyChanged(n);
            }

            return true;
        }
        */
    }
}
