﻿using System;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using Ninject;

namespace CrossApplication.Core.Net.Common.Container
{
    public class NinjectServiceLocator : ServiceLocatorImplBase
    {
        public NinjectServiceLocator(IKernel kernel)
        {
            _kernel = kernel;
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return _kernel.Get(serviceType, key);
        }

        private readonly IKernel _kernel;
    }
}