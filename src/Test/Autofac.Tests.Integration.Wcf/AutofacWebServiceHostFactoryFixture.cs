﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Autofac.Integration.Wcf;
using Autofac.Builder;
using Autofac.Core;

namespace Autofac.Tests.Integration.Wcf
{
    [TestFixture]
    public class AutofacWebServiceHostFactoryFixture
    {
        static readonly Uri[] DummyEndpoints = new[] { new Uri("http://localhost") };

        [Test]
        public void HostsKeyedServices()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<object>().Named<object>("service");
            TestWithHostedContainer(builder.Build(), () =>
            {
                var factory = new AutofacWebServiceHostFactory();
                var host = factory.CreateServiceHost("service", DummyEndpoints);
                Assert.IsNotNull(host);
            });
        }

        [Test]
        public void HostsTypedServices()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<object>();
            TestWithHostedContainer(builder.Build(), () =>
            {
                var factory = new AutofacWebServiceHostFactory();
                var host = factory.CreateServiceHost(typeof(object).FullName, DummyEndpoints);
                Assert.IsNotNull(host);
            });
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DetectsUnknownImplementationTypes()
        {
            var builder = new ContainerBuilder();
            builder.Register<ITestService>(c => new TestService()).Named<object>("service");
            TestWithHostedContainer(builder.Build(), () =>
            {
                var factory = new AutofacWebServiceHostFactory();
                factory.CreateServiceHost("service", DummyEndpoints);
            });
        }

        static void TestWithHostedContainer(IContainer container, Action test)
        {
            AutofacHostFactory.RootLifetimeScope = container;
            try
            {
                test();
            }
            finally
            {
                AutofacHostFactory.RootLifetimeScope = null;
            }
        }
    }
}
