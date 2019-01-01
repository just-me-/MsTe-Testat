using System;
using System.ServiceModel;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Common.FaultExceptions;
using AutoReservation.Dal;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public abstract class ManagerBase
    {

        protected static T UsingContext<T>(Func<AutoReservationContext, T> func)
        {
            using (var context = new AutoReservationContext())
            {
                return func(context);
            }
        }
        protected static T UpdateEntityWithoutReferences<T>(T entity, EntityState state) where T : class
        {
            return UsingContext(context =>
            {
                context.Entry(entity).State = state;
                SaveChanges(context, entity);

                return entity;
            });
        }
        protected static void SaveChanges<T>(AutoReservationContext context, T entity) where T : class
        {

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                OptimisticConcurrencyFault ex = new OptimisticConcurrencyFault
                {
                    Message = "Unbekannter Concurrency Fehler"
                };

                throw new FaultException<OptimisticConcurrencyFault>(ex);
            }
        }
        
    }
}