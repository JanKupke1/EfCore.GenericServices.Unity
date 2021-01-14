// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT licence. See License.txt in the project root for license information.

using System;
using GenericServices.Configuration;
using GenericServices.PublicButHidden;
using Unity;

namespace GenericServices.Setup.Internal
{
    /// <summary>
    /// Used to chain ConfigureGenericServicesEntities to RegisterGenericServices
    /// </summary>
    public class GenericServicesSetupPart2 : IGenericServicesSetupPart2
    {

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="services"></param>
        /// <param name="publicConfig"></param>
        /// <param name="configAndMapper"></param>
        /// <exception cref="ArgumentNullException"></exception>
        internal GenericServicesSetupPart2(IUnityContainer services, IGenericServicesConfig publicConfig, IWrappedConfigAndMapper configAndMapper)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            PublicConfig = publicConfig ?? throw new ArgumentNullException(nameof(publicConfig));
            ConfigAndMapper = configAndMapper ?? throw new ArgumentNullException(nameof(configAndMapper));
        }

        /// <inheritdoc />
        public IUnityContainer Services { get; }

        /// <inheritdoc />
        public IGenericServicesConfig PublicConfig { get; }

        /// <inheritdoc />
        public IWrappedConfigAndMapper ConfigAndMapper { get; }
    }
}