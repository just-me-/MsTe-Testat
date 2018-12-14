using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoReservation.Common;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using System.ServiceModel;
using AutoReservation.Common.Interfaces;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.UI
{
    public class AutoViewModel
    {
        ObservableCollection<KundeDto> Autos { get; set; }

        public AutoViewModel()
        {
            connectToServer();
        }

        private void connectToServer()
        {
            var channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
            var service = channelFactory.CreateChannel();

            Console.WriteLine("Service started");
            List<AutoDto> allAutos = service.GetAllAutoDtos();
            Autos = new ObservableCollection<KundeDto>(allAutos);
        }

    }
}
