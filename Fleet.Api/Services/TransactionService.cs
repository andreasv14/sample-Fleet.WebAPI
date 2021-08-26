using Fleet.Api.Factories;
using Fleet.Data;
using Fleet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fleet.Api.Services
{
    public class TransactionService : BaseDataService<Transaction>, ITransactionService
    {
        private readonly FleetDbContext _context;
        private readonly ITransactionModelFactory _transactionModelFactory;

        public TransactionService(FleetDbContext context,
            ITransactionModelFactory transactionModelFactory) : base(context)
        {
            _context = context;
            _transactionModelFactory = transactionModelFactory;
        }

        public async Task AddAsync(Transportation fromTransportation, Transportation toTransportation)
        {
            if (!fromTransportation.LoadContainers.Any())
            {
                throw new InvalidOperationException($"From transportation {fromTransportation.Id} does not have load containers");
            }

            var loadContainers = fromTransportation.LoadContainers.ToList();

            fromTransportation.LoadContainers = new List<Container>();

            // todo: Check delivered transportation if has capacity

            foreach (var container in loadContainers)
            {
                toTransportation.LoadContainers.Add(container);
            }

            _context.Transportations.Update(fromTransportation);
            _context.Transportations.Update(toTransportation);

            await _context.Transactions.AddAsync(_transactionModelFactory.Create(fromTransportation, toTransportation));

            await _context.SaveChangesAsync();
        }
    }
}
