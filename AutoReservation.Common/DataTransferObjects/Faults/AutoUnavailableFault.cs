using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
    [DataContract]
    public class AutoUnavailableFault
    {

        public AutoUnavailableFault()
        {
            Message = "Das Auto ist nicht verfügbar";
        }

        [DataMember]
        public AutoDto Auto { get; set; }

        [DataMember]
        public DateTime Von { get; set; }

        [DataMember]
        public DateTime Bis { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
