﻿using System.Collections.Generic;
using System.Linq;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;

namespace AutoReservation.BusinessLayer
{
    public class KundeManager : ManagerBase
    {
        public List<Kunde> List
        {
            get
            {
                using (AutoReservationContext context = new AutoReservationContext())
                {
                    return context.Kunden.ToList();
                }
            }
        }
        public Kunde GetKundeById(int id)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                return context.Kunden.FirstOrDefault(kunde => kunde.Id == id);
            }
        }
        public Kunde InsertKunde(Kunde kunde)
        {
            return updateKunde(kunde, EntityState.Added);
        }
        public Kunde UpdateKunde(Kunde kunde)
        {
            return updateKunde(kunde, EntityState.Modified);
        }
        public void DeleteKunde(Kunde kunde)
        {
            updateKunde(kunde, EntityState.Deleted);
        }

        private static Kunde updateKunde(Kunde value, EntityState state)
        {
            return updateEntityWithoutReferences(value, state);
        }


    }
}