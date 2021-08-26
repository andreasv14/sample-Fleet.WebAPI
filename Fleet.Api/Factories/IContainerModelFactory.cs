using Fleet.Models;

namespace Fleet.Api.Factories
{
    /// <summary>
    /// Factory for container model.
    /// </summary>
    public interface IContainerModelFactory
    {
        /// <summary>
        /// Creates new container model.
        /// </summary>
        /// <param name="containerName">Container name.</param>
        /// <returns>ContainerModel.</returns>
        Container Create(string containerName);
    }
}
