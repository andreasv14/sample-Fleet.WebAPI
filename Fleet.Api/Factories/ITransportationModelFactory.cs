using Fleet.Dtos;
using Fleet.Models;
using Fleet.Models.Enums;

namespace Fleet.Api.Factories
{
    /// <summary>
    /// Factory for transportation model.
    /// </summary>
    public interface ITransportationModelFactory
    {
        /// <summary>
        /// Create new transportation model.
        /// </summary>
        /// <param name="type">Transportation type.</param>
        /// <param name="name">Name.</param>
        /// <param name="capacity">Capacity.</param>
        /// <returns></returns>
        Transportation Create(TransportationType type, string name, uint capacity);
    }
}
