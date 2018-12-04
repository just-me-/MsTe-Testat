using System.Collections.Generic;
using System.Linq;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public class AutoManager : ManagerBase
    {
        public List<Auto> List
        {
            get
            {
                return usingContext(context => context.Autos.ToList());
            }
        }
        public Auto GetAutoById(int id)
        {
            return usingContext(context => context.Autos.FirstOrDefault(auto => auto.Id == id));
        }
        public Auto InsertAuto(Auto auto)
        {
            return updateAuto(auto, EntityState.Added);
        }
        public Auto UpdateAuto(Auto auto)
        {
            return updateAuto(auto, EntityState.Modified);
        }
        public void DeleteAuto(Auto auto)
        {
            updateAuto(auto, EntityState.Deleted);
        }

        private static Auto updateAuto(Auto value, EntityState state)
        {
            return updateEntityWithoutReferences(value, state);
        }

    }
}