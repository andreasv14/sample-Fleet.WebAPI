using Fleet.Api.Exceptions;
using Fleet.Data;
using Fleet.Domain;
using Fleet.Domain.Enums;
using Fleet.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fleet.Api.Services
{
    public class TransportationService : BaseDataService<Transportation>, ITransportationService
    {
        #region Private fields

        private readonly FleetDbContext _context;

        #endregion

        #region Constructors

        public TransportationService(FleetDbContext context) : base(context)
        {
            _context = context;
        }

        #endregion

        #region Public methods

        public async Task<IEnumerable<Transportation>> GetByTypeAsync(TransportationType type)
        {
            return await _context.Transportations
                .Include(transportation=>transportation.LoadContainers)
                .Where(transportation => transportation.Type == type)
                .ToListAsync();    
        }

        public async Task LoadAsync(int transportationId, IEnumerable<Container> containers)
        {
            var currentTransportation = await GetByIdAsync(transportationId);

            if (!containers.Any())
            {
                throw new ValidationException($"Containers list is empty");
            }

            if (!HasTransportationCapacity(currentTransportation, containers.Count()))
            {
                throw new ValidationException($"Transport {currentTransportation.Name} capacity is exceeded");
            }

            foreach (var container in containers)
            {
                currentTransportation.LoadContainers.Add(container);
            }

            _context.Update(currentTransportation);

            await _context.SaveChangesAsync();
        }

        public override async Task<Transportation> GetByIdAsync(int id)
        {
            var selectedTransportation = await _context.Transportations
                .Include(transportation => transportation.LoadContainers)
                .FirstOrDefaultAsync(transportation => transportation.Id == id);

            if (selectedTransportation == null)
            {
                throw new ObjectNotFoundException($"Transportation with Id {id} is not found");
            }

            return selectedTransportation;
        }

        public override async Task<IEnumerable<Transportation>> GetAllAsync()
        {
            return await _context.Transportations.Include(t => t.LoadContainers).ToListAsync();
        }

        #endregion

        #region Private methods

        private bool HasTransportationCapacity(Transportation transportation, int totalNewContainers)
        {
            var totalExistLoadContainers = transportation.LoadContainers.Count;

            var totalLoadContainers = totalNewContainers + totalExistLoadContainers;

            return totalLoadContainers <= transportation.Capacity;
        }

        #endregion
    }
}
