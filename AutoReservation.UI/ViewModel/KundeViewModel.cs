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
    public class KundeViewModel
    {
        ObservableCollection<Kunde> Kunden { get; set; }

        public KundeViewModel()
        {
            connectToServer();
        }

        private void connectToServer()
        {
            //so würde es bei http gehen:
            //var bind = new BasicHttpBinding();
            //var addr = new EndpointAddress("net.tcp://localhost:7876/AutoReservationService");
            //var factory = new ChannelFactory<IAutoReservationService>(bind, addr);

            //IAutoReservationService proxy = factory.CreateChannel();


            //Channel erstellen
            var channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
            var counterService = channelFactory.CreateChannel();


            //Testen und ConosleLog:
            Console.WriteLine("Service started");
            List<KundeDto> alleKunden = counterService.GetAllKundenDtos();

            alleKunden.ForEach((dto) =>
            {
                Console.WriteLine("ID: " + dto.Id + ", Nachname: " + dto.Nachname);
            }
            
            );


        }



        /*
        private void InitKunden()
        {
            Kunden = new ObservableCollection<Kunde>();
            Kunde k1 = new Kunde
            {
                Id = 1,
                Name = "Kistler",
                Vorname = "Tomtom",
                Geburtsdatum = new DateTime(1986, 9, 21),
                Timestamp = DateTime.Today()
            };

            Kunde k2 = new Kunde
            {
                Id = 2,
                Name = "Hess",
                Vorname = "Marcel",
                Geburtsdatum = new DateTime(1997, 2, 21),
                Timestamp = DateTime.Today
            };

            Kunde k3 = new Kunde

            {
                Id = 3,
                Name = "Reifler",
                Vorname = "Mamamamamarco",
                Geburtsdatum = new DateTime(1988, 12, 21),
                Timestamp = DateTime.Today
            };
            kunden.Add(k1);
            kunden.Add(k2);
            kunden.Add(k3);

        }
    }*/
    }
}
