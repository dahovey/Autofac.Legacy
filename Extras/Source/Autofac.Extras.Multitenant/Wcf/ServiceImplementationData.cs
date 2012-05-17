﻿using System;
using System.Security;
using Autofac;

namespace Autofac.Extras.Multitenant.Wcf
{
    /// <summary>
    /// Contains data about a WCF service implementation.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This data transfer object class is used in conjunction with
    /// <see cref="Autofac.Extras.Multitenant.Wcf.IServiceImplementationDataProvider"/>
    /// implementations for resolving service implementation instance information.
    /// </para>
    /// </remarks>
    /// <seealso cref="Autofac.Extras.Multitenant.Wcf.IServiceImplementationDataProvider"/>
    public class ServiceImplementationData
    {
        /// <summary>
        /// Gets or sets the string used to generate the data.
        /// </summary>
        /// <value>
        /// A <see cref="System.String"/> used to generate this service
        /// implementation data.
        /// </value>
        public string ConstructorString { get; set; }

        /// <summary>
        /// Gets or sets the concrete type to host.
        /// </summary>
        /// <value>
        /// A <see cref="System.Type"/> that indicates the type to pass to the
        /// service host when it is initially created. This type must be a concrete
        /// class and not an interface.
        /// </value>
        public Type ServiceTypeToHost { get; set; }

        /// <summary>
        /// Gets or sets a mechanism that allows the <see cref="AutofacInstanceContext"/>
        /// to get the actual implementation for a service.
        /// </summary>
        /// <value>
        /// An <see cref="System.Func{T,U}"/> that takes in a lifetime scope returns
        /// an <see cref="System.Object"/> that is the implementation type for the
        /// given service. This is the object that the service host will use
        /// and should be assignable from the <see cref="Autofac.Extras.Multitenant.Wcf.ServiceImplementationData.ServiceTypeToHost"/>.
        /// </value>
        public Func<ILifetimeScope, object> ImplementationResolver
        {
            get;

            set;
        }
    }
}
