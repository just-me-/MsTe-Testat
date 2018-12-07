﻿using System;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public abstract class ManagerBase
    {
        protected static OptimisticConcurrencyException<T> CreateOptimisticConcurrencyException<T>(AutoReservationContext context, T entity)
            where T : class
        {
            T dbEntity = (T)context.Entry(entity)
                .GetDatabaseValues()
                .ToObject();

            return new OptimisticConcurrencyException<T>($"Update {typeof(T).Name}: Concurrency-Fehler", dbEntity);
        }

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
                throw CreateOptimisticConcurrencyException(context, entity);
            }
        }
        
    }
}