using System.Collections.Generic;

namespace Fleet.Dtos
{
    public class TransactionDto
    {
        public int TransportationId { get; set; }

        public IEnumerable<int> ContainerIds { get; set; }
    }
}
