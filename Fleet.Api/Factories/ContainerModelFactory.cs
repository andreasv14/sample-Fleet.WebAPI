using Fleet.Models;

namespace Fleet.Api.Factories
{
    public class ContainerModelFactory : IContainerModelFactory
    {
        public Container Create(string containerName)
        {
            return new Container { Name = containerName };
        }
    }
}
