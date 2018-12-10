using System;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class AutoUpdateTests
        : TestBase
    {
        private AutoManager target;
        private AutoManager Target => target ?? (target = new AutoManager());

        [Fact]
        public void UpdateAutoTest()
        {
            Auto myCar = AutoManager.GetAutoById(1);
            myCar.Marke = "Marke";
            myCar.Tagestarif = 20;
            AutoManager.UpdateAuto(myCar);

            Auto sameCar = AutoManager.GetAutoById(1);
            Assert.Equal("Marke", myCar.Marke);
            Assert.Equal("Marke", sameCar.Marke);
            Assert.Equal(20, myCar.Tagestarif);
            Assert.Equal(20, sameCar.Tagestarif);
        }
    }
}
