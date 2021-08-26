using Fleet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fleet.Api.Services
{
    public interface IContainerService : IDataService<Container>
    {
        /// <summary>
        /// Get load containers of the current transportation async.
        /// </summary>
        /// <param name="transportationId">transportation Id.</param>
        /// <returns>List of containers.</returns>
        Task<IEnumerable<Container>> GetTransportLoadContainersAsync(int transportationId);
    }
}
