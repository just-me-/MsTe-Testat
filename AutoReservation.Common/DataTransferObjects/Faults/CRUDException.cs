using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Common.DataTransferObjects.Faults
{
    class CRUDException
    {
        public string Operation { get; set; }
        public string Description { get; set; }
        public int ErrorCode { get; set; }
    }
}
