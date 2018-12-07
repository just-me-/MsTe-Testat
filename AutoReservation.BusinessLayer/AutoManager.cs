using System.Collections.Generic;
using System.Linq;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public class AutoManager : ManagerBase
    {
        public static List<Auto> GetAllAutos()
        {
            return UsingContext(context => context.Autos.ToList());
        }
        public static Auto GetAutoById(int id)
        {
            return UsingContext(context => context.Autos.FirstOrDefault(auto => auto.Id == id));
        }
        public static Auto InsertAuto(Auto auto)
        {
            return UpdateAuto(auto, EntityState.Added);
        }
        public static Auto UpdateAuto(Auto auto)
        {
            return UpdateAuto(auto, EntityState.Modified);
        }
        public static void DeleteAuto(Auto auto)
        {
            UpdateAuto(auto, EntityState.Deleted);
        }

        private static Auto UpdateAuto(Auto value, EntityState state)
        {
            return UpdateEntityWithoutReferences(value, state);
        }

    }
}