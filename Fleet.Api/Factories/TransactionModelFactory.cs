using Fleet.Domain;
using Fleet.Domain.Enums;

namespace Fleet.Api.Factories
{
    public class TransactionModelFactory : ITransactionModelFactory
    {
        public Transaction Create(Transportation fromTransportation, Transportation toTransportation)
        {
            return new Transaction
            {
                Type = TransactionType.Transfer,
                FromTransportationId = fromTransportation.Id,
                FromTransportation = fromTransportation,
                ToTransportationId = toTransportation.Id,
                ToTransportation = toTransportation,
            };
        }
    }
}
