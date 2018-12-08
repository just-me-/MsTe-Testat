using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
    [DataContract]
    public class InvalidDateRangeFault
    {
        public InvalidDateRangeFault()
        {
            Message = "Reservation muss min 24h dauern und  Von-Datum muss vor bis-Datum liegen";
        }

        [DataMember]
        public  string Message { get; set; }

        [DataMember]
        public DateTime Von { get; set; }

        [DataMember]
        public DateTime Bis { get; set; }
    }
}
