﻿using System.Collections.Generic;
using System.Linq;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public class KundeManager : ManagerBase
    {
        public static List<Kunde> List
        {
            get
            {
                return UsingContext(context => context.Kunden.ToList());
            }
        }
        public static Kunde GetKundeById(int id)
        {
            return UsingContext(context => context.Kunden.FirstOrDefault(kunde => kunde.Id == id));
        }
        public static Kunde InsertKunde(Kunde kunde)
        {
            return updateKunde(kunde, EntityState.Added);
        }
        public static Kunde UpdateKunde(Kunde kunde)
        {
            return updateKunde(kunde, EntityState.Modified);
        }
        public static void DeleteKunde(Kunde kunde)
        {
            updateKunde(kunde, EntityState.Deleted);
        }

        private static Kunde updateKunde(Kunde value, EntityState state)
        {
            return UpdateEntityWithoutReferences(value, state);
        }


    }
}