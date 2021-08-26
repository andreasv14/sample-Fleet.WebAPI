using Fleet.Domain;
using Fleet.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fleet.Api.Services
{
    public interface ITransportationService : IDataService<Transportation>
    {
        /// <summary>
        /// Get transportations based on the type async.
        /// </summary>
        /// <param name="type">Transportation type.</param>
        /// <returns></returns>
        Task<IEnumerable<Transportation>> GetByTypeAsync(TransportationType type);

        /// <summary>
        /// Load containers into the current transportation.
        /// </summary>
        /// <param name="transportationId">Transportation Id</param>
        /// <param name="containers">List of containers.</param>
        /// <returns></returns>
        Task LoadAsync(int transportationId, IEnumerable<Container> containers);
    }
}
