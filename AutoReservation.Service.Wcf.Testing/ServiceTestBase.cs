using System;
using System.ServiceModel;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Common.FaultExceptions;
using AutoReservation.Common.Interfaces;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.Service.Wcf.Testing
{
    public abstract class ServiceTestBase
        : TestBase
    {
        protected abstract IAutoReservationService Target { get; }

        #region Read all entities

        [Fact]
        public void GetAutosTest()
        {
            Assert.Equal(4, Target.GetAllAutoDtos().Count);
        }

        [Fact]
        public void GetKundenTest()
        {
            Assert.Equal(4, Target.GetAllKundenDtos().Count);
        }

        [Fact]
        public void GetReservationenTest()
        {
            Assert.Equal(4, Target.GetAllReservationDtos().Count);
        }

        #endregion

        #region Get by existing ID

        [Fact]
        public void GetAutoByIdTest()
        {
            var auto = Target.GetAutoDtoById(1);
            Assert.Equal(1, auto.Id);
            Assert.Equal("Fiat Punto", auto.Marke);
            Assert.Equal(AutoKlasse.Standard, auto.AutoKlasse);
        }

        [Fact]
        public void GetKundeByIdTest()
        {
            var kunde = Target.GetKundeDtoById(1);
            Assert.Equal(1, kunde.Id);
        }

        [Fact]
        public void GetReservationByNrTest()
        {
            var res1 = Target.GetReservationDtoById(1);
            Assert.Equal(1, res1.Auto.Id);
        }

        #endregion

        #region Get by not existing ID

        [Fact]
        public void GetAutoByIdWithIllegalIdTest()
        {
            var auto = Target.GetAutoDtoById(-1);
            Assert.Null(auto);
        }

        [Fact]
        public void GetKundeByIdWithIllegalIdTest()
        {
            var kunde = Target.GetKundeDtoById(-1);
            Assert.Null(kunde);
        }

        [Fact]
        public void GetReservationByNrWithIllegalIdTest()
        {
            var reservation = Target.GetReservationDtoById(-1);
            Assert.Null(reservation);
        }

        #endregion

        #region Insert

        [Fact]
        public void InsertAutoTest()
        {
            var count = Target.GetAllAutoDtos().Count;
            var auto = new AutoDto();
            auto.AutoKlasse = AutoKlasse.Luxusklasse;
            auto.Marke = "Test Luxus";
            auto.Basistarif = 4242;
            Target.InsertAuto(auto);
            Assert.Equal(count + 1, Target.GetAllAutoDtos().Count);
        }

        [Fact]
        public void InsertKundeTest()
        {
            var count = Target.GetAllKundenDtos().Count;
            var kunde = new KundeDto();
            kunde.Geburtsdatum = new DateTime(1988, 12, 30);
            kunde.Vorname = "Marco";
            kunde.Nachname = "Polo";
            Target.InsertKunde(kunde);
            Assert.Equal(count + 1, Target.GetAllKundenDtos().Count);
        }

        [Fact]
        public void InsertReservationTest()
        {
            var count = Target.GetAllReservationDtos().Count;
            var res = new ReservationDto();
            res.Kunde = Target.GetKundeDtoById(1);
            res.Auto = Target.GetAutoDtoById(1);
            res.Von = new DateTime(2018, 12, 1);
            res.Bis = new DateTime(2018, 12, 12);
            Target.InsertReservation(res);
            Assert.Equal(count + 1, Target.GetAllReservationDtos().Count);
        }

        #endregion

        #region Delete  

        [Fact]
        public void DeleteAutoTest()
        {
            var count = Target.GetAllAutoDtos().Count;
            var auto = Target.GetAutoDtoById(1);
            Target.DeleteAuto(auto);
            Assert.Equal(count - 1, Target.GetAllAutoDtos().Count);
            Assert.Null(Target.GetAutoDtoById(1));
        }

        [Fact]
        public void DeleteKundeTest()
        {
            var count = Target.GetAllKundenDtos().Count;
            var kunde = Target.GetKundeDtoById(1);
            Target.DeleteKunde(kunde);
            Assert.Equal(count - 1, Target.GetAllKundenDtos().Count);
            Assert.Null(Target.GetKundeDtoById(1));
        }

        [Fact]
        public void DeleteReservationTest()
        {
            var count = Target.GetAllReservationDtos().Count;
            var reservation = Target.GetReservationDtoById(1);
            Target.DeleteReservation(reservation);
            Assert.Equal(count - 1, Target.GetAllReservationDtos().Count);
            Assert.Null(Target.GetReservationDtoById(1));
        }

        #endregion

        #region Update

        [Fact]
        public void UpdateAutoTest()
        {
            var upd = Target.GetAutoDtoById(1);
            upd.Marke = "TestCar";
            Target.UpdateAuto(upd);
            Assert.Equal("TestCar", Target.GetAutoDtoById(1).Marke);
        }

        [Fact]
        public void UpdateKundeTest()
        {
            var upd = Target.GetKundeDtoById(1);
            upd.Vorname = "Test";
            Target.UpdateKunde(upd);
            Assert.Equal("Test", Target.GetKundeDtoById(1).Vorname);
        }

        [Fact]
        public void UpdateReservationTest()
        {
            var upd = Target.GetReservationDtoById(1);
            upd.Von = new DateTime(2005, 1, 1);
            upd.Kunde = Target.GetKundeDtoById(4);
            Target.UpdateReservation(upd);
            Assert.Equal(new DateTime(2005, 1, 1), Target.GetReservationDtoById(1).Von);
            Assert.Equal(Target.GetKundeDtoById(4), Target.GetReservationDtoById(1).Kunde);
        }

        #endregion

        #region Update with optimistic concurrency violation

        [Fact]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            var mod1 = Target.GetAutoDtoById(1);
            var mod2 = Target.GetAutoDtoById(1);

            mod1.Marke = "FirstCar";
            Target.UpdateAuto(mod1);

            mod2.Marke = "SecondCar";
            Assert.Throws<FaultException<OptimisticConcurrencyFault>>(() => Target.UpdateAuto(mod2));
        }

        [Fact]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            var mod1 = Target.GetKundeDtoById(1);
            var mod2 = Target.GetKundeDtoById(1);

            mod1.Vorname = "FirstKunde";
            Target.UpdateKunde(mod1);

            mod2.Vorname = "SecondKunde";
            Assert.Throws<FaultException<OptimisticConcurrencyFault>>(() => Target.UpdateKunde(mod2));
        }

        [Fact]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            var mod1 = Target.GetReservationDtoById(1);
            var mod2 = Target.GetReservationDtoById(1);

            mod1.Bis = new DateTime(2020, 2, 2);
            Target.UpdateReservation(mod1);

            mod2.Bis = new DateTime(2020,2, 2);
            Assert.Throws<FaultException<OptimisticConcurrencyFault>>(() => Target.UpdateReservation(mod2));
        }

        #endregion


        #region Insert / update invalid time range

        [Fact]
        public void InsertReservationWithInvalidDateRangeTest()
        {
            var res = new ReservationDto
            {
                Kunde = Target.GetKundeDtoById(1),
                Auto = Target.GetAutoDtoById(1),
                Von = new DateTime(2018, 12, 13),
                Bis = new DateTime(2018, 12, 12)
            };
            Assert.Throws<FaultException<InvalidDateRangeFault>>(() => Target.InsertReservation(res));
        }

        [Fact]
        public void InsertReservationWithAutoNotAvailableTest()
        {
            var res = new ReservationDto
            {
                Kunde = Target.GetKundeDtoById(1),
                Auto = Target.GetAutoDtoById(1),
                Von = new DateTime(2020, 1, 11),
                Bis = new DateTime(2020, 1, 19)
            };
            Assert.Throws<FaultException<AutoUnavailableFault>>(() => Target.InsertReservation(res));
        }


        [Fact]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
            var reservation = Target.GetReservationDtoById(1);
            reservation.Bis = new DateTime(2019, 1, 20);
            Assert.Throws<FaultException<InvalidDateRangeFault>>(() => Target.UpdateReservation(reservation));
        }

        [Fact]
        public void UpdateReservationWithAutoNotAvailableTest()
        {
            var reservation = Target.GetReservationDtoById(1);
            reservation.Auto = Target.GetAutoDtoById(2);
            Assert.Throws<FaultException<AutoUnavailableFault>>(() => Target.UpdateReservation(reservation));

        }

        #endregion

        //#region Check Availability

        //[Fact]
        //public void CheckAvailabilityIsTrueTest()
        //{
        //    throw new NotImplementedException("Test not implemented.");
        //}

        //[Fact]
        //public void CheckAvailabilityIsFalseTest()
        //{
        //    throw new NotImplementedException("Test not implemented.");
        //}

        //#endregion
    }
}
