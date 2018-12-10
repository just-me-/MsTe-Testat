using System;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationUpdateTest
        : TestBase
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());

        [Fact]
        public void UpdateReservationTest()
        {
            Reservation res = ReservationManager.GetReservationById(1);
            res.Von = DateTime.Today.AddDays(20);
            res.Bis = DateTime.Today.AddDays(30); ;
            res.KundeId = 2;
            res.AutoId = 3;
            ReservationManager.UpdateReservation(res);

            Reservation sameRes = ReservationManager.GetReservationById(1);

            Assert.Equal(2, sameRes.KundeId);
            Assert.Equal(3, sameRes.AutoId);
            Assert.Equal(DateTime.Today.AddDays(30), sameRes.Bis);
        }
    }
}
