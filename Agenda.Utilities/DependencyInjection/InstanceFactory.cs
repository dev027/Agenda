// <copyright file="InstanceFactory.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using Agenda.Utilities.Exceptions;
using SimpleInjector;

namespace Agenda.Utilities.DependencyInjection
{
    /// <summary>
    /// Instance Factory.
    /// </summary>
    public static class InstanceFactory
    {
        private static readonly Container Container = new Container();

        /// <summary>
        /// Return debug details of what is registered.
        /// </summary>
        /// <returns>List of debug details.</returns>
        public static IList<string> Debug()
        {
            InstanceProducer[] registrations = Container.GetCurrentRegistrations();

            return registrations.Select(instanceProducer =>
                    "(" + instanceProducer.Lifestyle + ") "
                    + instanceProducer.ServiceType + " => "
                    + instanceProducer.Registration.ImplementationType)
                .ToList();
        }

        /// <summary>
        /// Registers a type to fulfil instance requests (new instance every time).
        /// </summary>
        /// <typeparam name="TInterface">The interface to register for.</typeparam>
        /// <typeparam name="TImplementation">The implementation to fulfil the interface.</typeparam>
        /// <param name="isOverride">True to allow explicit override.</param>
        public static void RegisterTransient<TInterface, TImplementation>(bool isOverride = false)
            where TImplementation : class, TInterface
            where TInterface : class
        {
            Container.Options.AllowOverridingRegistrations = isOverride;
            Register<TInterface, TImplementation>(Lifestyle.Transient);
            Container.Options.AllowOverridingRegistrations = false;
        }

        /// <summary>
        /// Gets the registered implementation of the specified interface.
        /// </summary>
        /// <typeparam name="TInterface">The interface to request the registered implementation for.</typeparam>
        /// <returns>The registered implementation of the specified interface.</returns>
        public static TInterface GetInstance<TInterface>()
            where TInterface : class
        {
            try
            {
                // Validate that the type is an interface otherwise throw an exception
                IsTypeInterface(typeof(TInterface));

                return Container.GetInstance<TInterface>();
            }
            catch (ActivationException activationException)
            {
                throw new InstanceFactoryException(
                    $"An error occurred getting an instance of {typeof(TInterface).FullName}",
                    activationException);
            }
            catch (Exception e)
            {
                throw new InstanceFactoryException(e.Message, e);
            }
        }

        private static void IsTypeInterface(Type entity)
        {
            if (entity.IsInterface)
            {
                return;
            }

            throw new InstanceFactoryException(
                    $"The concrete class {entity.FullName} has been passed in as a type.  You must pass in the interface that the class implements");
        }

        private static void Register<TInterface, TImplementation>(Lifestyle lifestyle)
            where TImplementation : class, TInterface
            where TInterface : class
        {
            Container.Register<TInterface, TImplementation>(lifestyle);
            Container.Options.AllowOverridingRegistrations = false;
        }
    }
}