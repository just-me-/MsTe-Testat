using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoReservation.Common;
using System.ServiceModel;
using AutoReservation.Common.Interfaces;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.UI
{
    public class KundeViewModel
    {
        ObservableCollection<KundeDto> Kunden { get; set; }

        public KundeViewModel()
        {
            connectToServer();
        }

        private void connectToServer()
        {



            //Channel erstellen
            var channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
            var service = channelFactory.CreateChannel();


            //Testen und ConosleLog:
            Console.WriteLine("Service started");
            List<KundeDto> allCustomers = service.GetAllKundenDtos();

            Kunden = new ObservableCollection<KundeDto>(allCustomers);

            //allCustomers.ForEach((dto) =>
            //{
            //    Console.WriteLine("ID: " + dto.Id + ", Nachname: " + dto.Nachname);
            //}
            
            //);


        }

    }
}
