using System;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects.Faults;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationAvailabilityTest
        : TestBase
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());





        [Fact]
        public void TestOverlap1()
        {
            //    ------------1-------------
            //        --------------2-------------
            Reservation res = ReservationManager.GetReservationById(1);
            res.Von = DateTime.Today;
            res.Bis = DateTime.Today.AddDays(1);
            int targetCarId = res.AutoId;
            ReservationManager.UpdateReservation(res);

            Reservation res2 = ReservationManager.GetReservationById(2);
            res2.Von = DateTime.Today.AddHours(1);
            res2.Bis = DateTime.Today.AddDays(2);
            res2.AutoId = targetCarId;

            Assert.Throws<FaultException<AutoUnavailableFault>>(
             () => ReservationManager.UpdateReservation(res2)
            );
        }

        [Fact]
        public void TestOverlap2()
        {
            //             ------------1-------------
            //        --------------2-------------
            Reservation res = ReservationManager.GetReservationById(1);
            res.Von = DateTime.Today.AddHours(1);
            res.Bis = DateTime.Today.AddDays(2);
            int targetCarId = res.AutoId;
            ReservationManager.UpdateReservation(res);

            Reservation res2 = ReservationManager.GetReservationById(2);
            res2.Von = DateTime.Today;
            res2.Bis = DateTime.Today.AddDays(1);
            res2.AutoId = targetCarId;

            Assert.Throws<FaultException<AutoUnavailableFault>>(
             () => ReservationManager.UpdateReservation(res2)
            );
        }

        [Fact]
        public void TestOverlap3()
        {
            //    ------------------1------------------
            //        --------------2-------------
            Reservation res = ReservationManager.GetReservationById(1);
            res.Von = DateTime.Today;
            res.Bis = DateTime.Today.AddDays(5);
            int targetCarId = res.AutoId;
            ReservationManager.UpdateReservation(res);

            Reservation res2 = ReservationManager.GetReservationById(2);
            res2.Von = DateTime.Today.AddDays(1);
            res2.Bis = DateTime.Today.AddDays(2);
            res2.AutoId = targetCarId;

            Assert.Throws<FaultException<AutoUnavailableFault>>(
             () => ReservationManager.UpdateReservation(res2)
            );
        }



        [Fact]
        public void TestOverlap4()
        {
            //          ------------1-------------
            //      ----------------2-----------------
            Reservation res = ReservationManager.GetReservationById(1);
            res.Von = DateTime.Today.AddDays(1);
            res.Bis = DateTime.Today.AddDays(2);
            int targetCarId = res.AutoId;
            ReservationManager.UpdateReservation(res);

            Reservation res2 = ReservationManager.GetReservationById(2);
            res2.Von = DateTime.Today;
            res2.Bis = DateTime.Today.AddDays(5);
            res2.AutoId = targetCarId;

            Assert.Throws<FaultException<AutoUnavailableFault>>(
             () => ReservationManager.UpdateReservation(res2)
            );
        }


        [Fact]
        public void TestOK1()
        {
            //          ------------1-------------
            //                                    ------------2-------------
            Reservation res = ReservationManager.GetReservationById(1);
            res.Von = DateTime.Today.AddDays(1);
            res.Bis = DateTime.Today.AddDays(2);
            ReservationManager.UpdateReservation(res);

            Reservation res2 = ReservationManager.GetReservationById(2);
            res2.Von = DateTime.Today.AddDays(2);
            res2.Bis = DateTime.Today.AddDays(3);

            ReservationManager.UpdateReservation(res2);
        }

        [Fact]
        public void TestOK2()
        {
            //                                   ------------1-------------
            //         ------------2-------------
            Reservation res = ReservationManager.GetReservationById(1);
            res.Von = DateTime.Today.AddDays(2);
            res.Bis = DateTime.Today.AddDays(3);
            ReservationManager.UpdateReservation(res);

            Reservation res2 = ReservationManager.GetReservationById(2);
            res2.Von = DateTime.Today.AddDays(1);
            res2.Bis = DateTime.Today.AddDays(2);

            ReservationManager.UpdateReservation(res2);
        }


    }
}
