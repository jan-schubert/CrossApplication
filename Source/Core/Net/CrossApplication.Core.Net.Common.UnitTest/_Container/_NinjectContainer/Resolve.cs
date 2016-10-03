﻿using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Net.Common.Container;
using CrossApplication.Core.Net.Common.UnitTest._Container.TestClasses;
using FluentAssertions;
using Ninject;
using Xunit;

namespace CrossApplication.Core.Net.Common.UnitTest._Container._NinjectContainer
{
    public class Resolve
    {
        [Fact]
        public void Usage()
        {
            var container = new NinjectContainer(new StandardKernel());
            container.RegisterType<IInjectionInterface, InjectionClass>();

            var injectionObject = container.Resolve<IInjectionInterface>();

            injectionObject.Should().NotBeNull();
        }

        [Fact]
        public void ShouldResolveDifferentInstances()
        {
            var container = new NinjectContainer(new StandardKernel());
            container.RegisterType<IInjectionInterface, InjectionClass>();

            var injectionObject = container.Resolve<IInjectionInterface>();
            var injectionObject2 = container.Resolve<IInjectionInterface>();

            injectionObject.Should().NotBeNull();
            injectionObject2.Should().NotBeNull();
            injectionObject.Should().NotBe(injectionObject2);
        }

        [Fact]
        public void ShouldResolveSameInstance()
        {
            var container = new NinjectContainer(new StandardKernel());
            container.RegisterType<IInjectionInterface, InjectionClass>(Lifetime.PerContainer);

            var injectionObject = container.Resolve<IInjectionInterface>();
            var injectionObject2 = container.Resolve<IInjectionInterface>();

            injectionObject.Should().NotBeNull();
            injectionObject2.Should().NotBeNull();
            injectionObject.Should().Be(injectionObject2);
        }

        [Fact]
        public void ShouldResolveSameInstanceIfInstanceIsRegistered()
        {
            var container = new NinjectContainer(new StandardKernel());
            container.RegisterInstance<IInjectionInterface>(new InjectionClass());

            var injectionObject = container.Resolve<IInjectionInterface>();
            var injectionObject2 = container.Resolve<IInjectionInterface>();

            injectionObject.Should().NotBeNull();
            injectionObject2.Should().NotBeNull();
            injectionObject.Should().Be(injectionObject2);
        }
    }
}