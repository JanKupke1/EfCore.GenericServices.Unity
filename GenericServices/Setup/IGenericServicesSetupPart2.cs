﻿using GenericServices.Unity.Configuration;
using GenericServices.Unity.PublicButHidden;
using Unity;

namespace GenericServices.Unity.Setup
{
    /// <summary>
    /// Used to chain ConfigureGenericServicesEntities to RegisterGenericServices
    /// </summary>
    public interface IGenericServicesSetupPart2 
    {
        /// <summary>
        /// The DI ServiceCollection to use for registering
        /// </summary>
        IUnityContainer Services { get; }

        /// <summary>
        /// Global GenericServices configuration
        /// </summary>
        IGenericServicesConfig PublicConfig { get; }

        /// <summary>
        /// The AutoMapper setting needed by GenericServices
        /// </summary>
        IWrappedConfigAndMapper ConfigAndMapper { get; }
    }
}