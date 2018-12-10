using System;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;
using AutoReservation.Common.DataTransferObjects.Faults;
using System.ServiceModel;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationDateRangeTest
        : TestBase
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());


        [Fact]
        public void TestExactly24hReservation()
        {
            Reservation res = ReservationManager.GetReservationById(1);
            res.Von = DateTime.Today;
            res.Bis = DateTime.Today.AddDays(1);
            ReservationManager.UpdateReservation(res);
        }


        [Fact]
        public void TestNot24h()
        {
            Reservation res = ReservationManager.GetReservationById(1);
            res.Von = DateTime.Today;
            res.Bis = DateTime.Today.AddHours(12);
    

            
            var ex = Assert.Throws<FaultException<InvalidDateRangeFault>>( 
                 () => ReservationManager.UpdateReservation(res)
                );

            Assert.Equal(ReservationManager.min24hMessage, ex.Detail.Message);

        }

        [Fact]
        public void TestVonAfterBis()
        {
            Reservation res = ReservationManager.GetReservationById(1);
            res.Von = DateTime.Today.AddDays(1);
            res.Bis = DateTime.Today;
            var ex = Assert.Throws<FaultException<InvalidDateRangeFault>>(
             () => ReservationManager.UpdateReservation(res)
             );
            Assert.Equal(ReservationManager.vonNachBisMessage, ex.Detail.Message);
        }


    }
}
