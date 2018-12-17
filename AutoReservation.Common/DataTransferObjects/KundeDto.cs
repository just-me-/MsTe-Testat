using System;
using System.Runtime.Serialization;
using PropertyChanged;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class KundeDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Vorname { get; set; }
        [DataMember]
        public string Nachname { get; set; }
        [DataMember]
        public DateTime Geburtsdatum { get; set; }
        [DataMember]
        public byte[] Timestamp { get; set; }


        //public override string ToString()
        //    => $"{Id}; {Nachname}; {Vorname}; {Geburtsdatum}; {Timestamp}";


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
