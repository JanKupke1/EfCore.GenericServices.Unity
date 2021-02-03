using GenericServices.Unity.Configuration;
using Unity;

namespace GenericServices.Unity.Setup
{
    /// <summary>
    /// Used to chain ConfigureGenericServicesEntities to ConfigureGenericServicesEntities
    /// </summary>
    public interface IGenericServicesSetupPart1
    {
        /// <summary>
        /// Global GenericServices configuration
        /// </summary>
        IGenericServicesConfig PublicConfig { get; }

        /// <summary>
        /// The DI ServiceCollection to use for registering
        /// </summary>
        IUnityContainer Services { get; }
    }
}