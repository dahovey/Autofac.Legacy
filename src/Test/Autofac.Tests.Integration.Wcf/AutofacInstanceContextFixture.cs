﻿using System.Linq;
using Autofac.Builder;
using Autofac.Integration.Wcf;
using NUnit.Framework;
using Autofac.Util;
using Autofac.Core;

namespace Autofac.Tests.Integration.Wcf
{
    [TestFixture]
    public class AutofacInstanceContextFixture
    {
        class DisposeTracker : Disposable
        {
            protected override void Dispose(bool disposing)
            {
                IsDisposed = true;
                base.Dispose(disposing);
            }

            public bool IsDisposed { get; private set; }
        }

        [Test]
        public void InstancesDisposed()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DisposeTracker>();
            var container = builder.Build();
            IComponentRegistration registration;
            container.ComponentRegistry.TryGetRegistration(new TypedService(typeof(DisposeTracker)), out registration);
            var context = new AutofacInstanceContext(container);
            var disposable = (DisposeTracker)context.ResolveComponent(registration, Enumerable.Empty<Parameter>());
            Assert.IsFalse(disposable.IsDisposed);
            context.Dispose();
            Assert.IsTrue(disposable.IsDisposed);
        }
    }
}
