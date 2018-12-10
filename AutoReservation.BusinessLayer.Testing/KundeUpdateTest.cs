using System;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class KundeUpdateTest
        : TestBase
    {
        private KundeManager target;
        private KundeManager Target => target ?? (target = new KundeManager());

        [Fact]
        public void UpdateKundeTest()
        {
            Kunde someDude = KundeManager.GetKundeById(1);
            someDude.Nachname = "DUDE!";
            KundeManager.UpdateKunde(someDude);

            someDude = KundeManager.GetKundeById(1);
            Assert.Equal("DUDE!", someDude.Nachname);
        }
    }
}
