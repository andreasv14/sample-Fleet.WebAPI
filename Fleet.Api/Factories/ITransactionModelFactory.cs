using Fleet.Domain;

namespace Fleet.Api.Factories
{
    public interface ITransactionModelFactory
    {
        Transaction Create(Transportation fromTransportation, Transportation toTransportation);
    }
}
