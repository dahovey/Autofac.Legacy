﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autofac.Integration.Web
{
    /// <summary>
    /// Does not inject properties.
    /// </summary>
    class NoInjection : IInjectionBehaviour
    {
        /// <summary>
        /// Inject dependencies in the required fashion.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="target">The target.</param>
        public void InjectDependencies(IContext context, object target)
        {
        }
    }
}
