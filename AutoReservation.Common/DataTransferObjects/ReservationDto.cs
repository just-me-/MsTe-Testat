using System;
using System.Runtime.Serialization;
using PropertyChanged;

namespace AutoReservation.Common.DataTransferObjects
{

    [DataContract]
    public class ReservationDto
    {
        [DataMember]
        public int ReservationsNr { get; set; }
        [DataMember]
        public DateTime Von { get; set; }
        [DataMember]
        public DateTime Bis { get; set; }
        [DataMember]
        public byte[] Timestamp { get; set; }
        [DataMember]
        public AutoDto Auto { get; set; }
        [DataMember]
        public KundeDto Kunde { get; set; }

        //public override string ToString()
        //    => $"{ReservationsNr}; {Von}; {Bis}; {Auto}; {Kunde}";

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
