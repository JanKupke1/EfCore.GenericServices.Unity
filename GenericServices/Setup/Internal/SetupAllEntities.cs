// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

using System;
using GenericServices.Configuration;
using Microsoft.EntityFrameworkCore;
using Unity;

namespace GenericServices.Setup.Internal
{
    internal class SetupAllEntities : IGenericServicesSetupPart1
    {
        public IGenericServicesConfig PublicConfig { get; }
        public IUnityContainer Services { get; }

        public SetupAllEntities(IUnityContainer services, IGenericServicesConfig publicConfig, Type[] contextTypes)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            PublicConfig = publicConfig ?? new GenericServicesConfig();
            if (contextTypes == null || contextTypes.Length <= 0)
                throw new ArgumentException(nameof(contextTypes));
            foreach (var contextType in contextTypes)
            {
                try
                {
                    using (var context = services.Resolve(contextType) as DbContext)
                    {
                        if (context == null)
                            throw new InvalidOperationException($"You provided the a DbContext called {contextType.Name}, but it doesn't seem to be registered, or is a DbContext. Have you forgotten to register it?");
                        context.RegisterEntityClasses();
                    }
                }
                catch (Exception ex)
                {
                    throw new InitializeServiceException("Setup of the service could not be run due to an exception", ex);
                }

            }
        }


    }
}