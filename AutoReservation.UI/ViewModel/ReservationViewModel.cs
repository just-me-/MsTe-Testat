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
    public class ReservationViewModel
    {
        ObservableCollection<ReservationDto> Reservation { get; set; }

        public ReservationViewModel()
        {
            connectToServer();
        }

        private void connectToServer()
        {
            var channelFactory = new ChannelFactory<IAutoReservationService>("AutoReservationService");
            var service = channelFactory.CreateChannel();

            Console.WriteLine("Service started");
            List<ReservationDto> allReservations = service.GetAllReservationDtos();

            Reservation = new ObservableCollection<ReservationDto>(allReservations);
        }

    }
}
