using Fleet.Data;
using Fleet.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fleet.Api.Services
{
    public class ContainerService : BaseDataService<Container>, IContainerService
    {
        private readonly FleetDbContext _context;

        public ContainerService(FleetDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Container>> GetTransportLoadContainersAsync(int transportationId)
        {
            return await _context.Containers
                .Include(container => container.Transportation)
                .Where(container => container.TransportationId == transportationId)
                .ToListAsync();
        }
    }
}
