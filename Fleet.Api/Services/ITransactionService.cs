using Fleet.Domain;
using System.Threading.Tasks;

namespace Fleet.Api.Services
{
    public interface ITransactionService : IDataService<Transaction>
    {
        Task AddAsync(Transportation fromTransportation, Transportation toTransportation);
    }
}
