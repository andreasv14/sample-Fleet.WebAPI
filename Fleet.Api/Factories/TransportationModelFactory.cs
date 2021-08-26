using Fleet.Models;
using Fleet.Models.Enums;

namespace Fleet.Api.Factories
{
    public class TransportationModelFactory : ITransportationModelFactory
    {
        public Transportation Create(TransportationType type, string name, uint capacity)
        {
            return new Transportation()
            {
                Name = name,
                Type = type,
                Capacity = capacity
            };
        }
    }
}
