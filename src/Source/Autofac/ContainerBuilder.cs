﻿// This software is part of the Autofac IoC container
// Copyright (c) 2007 - 2008 Autofac Contributors
// http://autofac.org
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using Autofac.Util;
using Autofac.Core;

namespace Autofac
{
	/// <summary>
	/// Used to build an <see cref="IContainer"/> from component registrations.
	/// </summary>
    /// <example>
    /// var builder = new ContainerBuilder();
    /// 
    /// builder.RegisterType&lt;Logger&gt;()
    ///     .As&lt;ILogger&gt;()
    ///     .SingleSharedInstance();
    /// 
    /// builder.RegisterDelegate(c => new MessageHandler(c.Resolve&lt;ILogger&gt;()));
    /// 
    /// var container = builder.Build();
    /// // resolve components from container...
    /// </example>
    /// <remarks>Most <see cref="ContainerBuilder"/> functionality is accessed
    /// via extension methods in <see cref="RegistrationExtensions"/>.</remarks>
    /// <seealso cref="IContainer"/>
    /// <see cref="RegistrationExtensions"/>
	public class ContainerBuilder
	{
        private readonly IList<Action<IComponentRegistry>> _configurationCallbacks = new List<Action<IComponentRegistry>>();
		private bool _wasBuilt;

        /// <summary>
        /// Register a callback that will be invoked when the container is configured.
        /// </summary>
        /// <remarks>This is primarily for extending the builder syntax.</remarks>
        /// <param name="configurationCallback">Callback to execute.</param>
        public virtual void RegisterCallback(Action<IComponentRegistry> configurationCallback)
        {
            _configurationCallbacks.Add(Enforce.ArgumentNotNull(configurationCallback, "configurationCallback"));
        }

		/// <summary>
		/// Create a new container with the component registrations that have been made.
		/// </summary>
		/// <remarks>
        /// Build can only be called once per <see cref="ContainerBuilder"/>
        /// - this prevents ownership issues for provided instances.
		/// </remarks>
		/// <returns>A new container with the configured component registrations.</returns>
		public virtual IContainer Build()
		{
			var result = new Container();
			Build(result.ComponentRegistry);
			return result;
		}

		/// <summary>
		/// Configure an existing component registry with the component registrations
        /// that have been made.
		/// </summary>
        /// <remarks>
        /// Build can only be called once per <see cref="ContainerBuilder"/>
        /// - this prevents ownership issues for provided instances.
        /// </remarks>
        /// <param name="componentRegistry">An existing component registry to make the registrations in.</param>
		public virtual void Build(IComponentRegistry componentRegistry)
		{
            Enforce.ArgumentNotNull(componentRegistry, "componentRegistry");

			if (_wasBuilt)
				throw new InvalidOperationException();

			_wasBuilt = true;

			foreach (var callback in _configurationCallbacks)
                callback(componentRegistry);
		}
	}
}