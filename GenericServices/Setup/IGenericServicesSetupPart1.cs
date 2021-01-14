using GenericServices.Configuration;
using Unity;

namespace GenericServices.Setup
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