using System;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects.Faults;
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
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void GetKundenTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void GetReservationenTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Get by existing ID

        [Fact]
        public void GetAutoByIdTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void GetKundeByIdTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void GetReservationByNrTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Get by not existing ID

        [Fact]
        public void GetAutoByIdWithIllegalIdTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void GetKundeByIdWithIllegalIdTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void GetReservationByNrWithIllegalIdTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Insert

        [Fact]
        public void InsertAutoTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void InsertKundeTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void InsertReservationTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Delete  

        [Fact]
        public void DeleteAutoTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void DeleteKundeTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void DeleteReservationTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Update

        [Fact]
        public void UpdateAutoTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateKundeTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateReservationTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Update with optimistic concurrency violation

        [Fact]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Insert / update invalid time range

        [Fact]
        public void InsertReservationWithInvalidDateRangeTest()
        {
            Auto auto = new StandardAuto();
            Kunde kunde = new Kunde();

            //weniger als 24h
            Reservation r = new Reservation
            {
                Auto = auto,
                Kunde = kunde,
                Von = new DateTime(2000, 6, 6, 12, 0, 0),
                Bis = new DateTime(2000, 6, 6, 12, 0, 1)
            };


            Target.InsertReservation(r.ConvertToDto());

            Assert.Throws<FaultException<InvalidDateRangeFault>>(
                    () => Target.InsertReservation(r.ConvertToDto())
                );

            //bis < von
            Reservation r2 = new Reservation
            {
                Auto = auto,
                Kunde = kunde,
                Von = new DateTime(2000, 6, 6, 12, 0, 0),
                Bis = new DateTime(1999, 6, 6, 12, 0, 1)
            };

            Assert.Throws<FaultException<InvalidDateRangeFault>>(
                () => Target.InsertReservation(r.ConvertToDto())
            );

        }

        [Fact]
        public void InsertReservationWithAutoNotAvailableTest()
        {

            Auto auto = new StandardAuto();
            Kunde kunde = new Kunde();

            Reservation r = new Reservation
            {
                Auto = auto,
                Kunde = kunde,
                Von = new DateTime(2000, 6, 6, 12, 0, 0),
                Bis = new DateTime(2000, 6, 7, 12, 0, 1)
            };

            Reservation r2 = new Reservation
            {
                Auto = auto,
                Kunde = kunde,
                Von = new DateTime(2000, 6, 7, 0, 0, 0),  //auto bis 7. um 12 reserviert, der will nun um 0 uhr reservieren
                Bis = new DateTime(2000, 6, 9, 12, 0, 1)
            };

            Target.InsertReservation(r.ConvertToDto());

            Assert.Throws<FaultException<AutoUnavailableFault>>(
                () => Target.InsertReservation(r2.ConvertToDto())
            );


        }


        [Fact]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
            Auto auto = new StandardAuto();
            Kunde kunde = new Kunde();
            Reservation r = new Reservation
            {
                Auto = auto,
                Kunde = kunde,
                Von = new DateTime(2000, 6, 6, 12, 0, 0),
                Bis = new DateTime(2000, 6, 7, 12, 0, 1)
            };

            Target.InsertReservation(r.ConvertToDto());

            r.Bis = new DateTime(2000, 6, 6, 12, 0, 1);
           

            Assert.Throws<FaultException<InvalidDateRangeFault>>(
                () => Target.UpdateReservation(r.ConvertToDto())
            );
        }

        [Fact]
        public void UpdateReservationWithAutoNotAvailableTest()
        {
            Auto auto = new StandardAuto();
            Kunde kunde = new Kunde();

            Reservation r = new Reservation
            {
                Auto = auto,
                Kunde = kunde,
                Von = new DateTime(2000, 6, 6, 12, 0, 0),
                Bis = new DateTime(2000, 6, 7, 12, 0, 1)
            };

            Reservation r2 = new Reservation
            {
                Auto = auto,
                Kunde = kunde,
                Von = new DateTime(2000, 6, 8, 0, 0, 0), 
                Bis = new DateTime(2000, 6, 9, 12, 0, 1)
            };

            Target.InsertReservation(r.ConvertToDto());
            Target.InsertReservation(r2.ConvertToDto());


            r2.Von = new DateTime(2000, 6, 7, 0, 0, 1);

            Assert.Throws<FaultException<AutoUnavailableFault>>(
                () => Target.UpdateReservation(r2.ConvertToDto())
            );
        }

        #endregion

        #region Check Availability

        [Fact]
        public void CheckAvailabilityIsTrueTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void CheckAvailabilityIsFalseTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion
    }
}
