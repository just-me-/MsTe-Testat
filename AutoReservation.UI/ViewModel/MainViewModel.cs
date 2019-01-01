using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.UI
{

    public class MainViewModel
    {
        public ObservableCollection<KundeDto> Kunden { get; set; }
        public ObservableCollection<AutoDto> Autos { get; set; }
        public ObservableCollection<ReservationDto> Reservation { get; set; }

        public AutoDto SelectedAuto { get; set; }
        public KundeDto SelectedKunde { get; set; }
        //^^^^ wenn wir im DataGrid SelectedItem="{Binding SelectedKunde}" hätten, könnte man das nutzen, machen wir aber mit list.SelectedItem

        public ChannelFactory<IAutoReservationService> channelFactory { get; set; }
        public IAutoReservationService service { get; set; }

        public MainViewModel()
        {
            channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
            service = channelFactory.CreateChannel();

            // tmp
            // needSomeDummyData();
            showMyData();

            Reservation = new ObservableCollection<ReservationDto>(service.GetAllReservationDtos());
            Kunden = new ObservableCollection<KundeDto>(service.GetAllKundenDtos());
            Autos = new ObservableCollection<AutoDto>(service.GetAllAutoDtos());
            // marke noch to string machen für die liste... nur für die liste 
            // Autos.ForEach(a => a.Marke = a.Marke.ToString())
            //(Toms antwort) --> AutoDto.Marke ist ein string ?!!=?

            SelectedAuto = new AutoDto();
            SelectedKunde = new KundeDto();

        }

        public void updateData()
        {
            Reservation = new ObservableCollection<ReservationDto>(service.GetAllReservationDtos());
            Kunden = new ObservableCollection<KundeDto>(service.GetAllKundenDtos());
            Autos = new ObservableCollection<AutoDto>(service.GetAllAutoDtos());
        }

        private void needSomeDummyData()
        {
            AutoDto auto = new AutoDto();
            auto.Marke="test";
            auto.Basistarif = 12;
            service.InsertAuto(auto);

            KundeDto kunde = new KundeDto();
            kunde.Vorname = "Peter";
            kunde.Nachname = "Muster";
            service.InsertKunde(kunde);
        }


        public void showMyData()
        {
            Console.WriteLine("====================Dump data===================");
            service.GetAllKundenDtos().ForEach((dto) =>
            {
                Console.WriteLine("Kunde ID: " + dto.Id + ", Nachname: " + dto.Nachname);
            });
            service.GetAllAutoDtos().ForEach((dto) =>
            {
                Console.WriteLine("Auto ID: " + dto.Id + ", Marke: " + dto.Marke);
            });
            Console.WriteLine("=================================================");
        }

    }
}
