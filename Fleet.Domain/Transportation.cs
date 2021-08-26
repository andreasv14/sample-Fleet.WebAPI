using Fleet.Domain.Enums;
using System.Collections.Generic;

namespace Fleet.Domain
{
    public class Transportation : BaseModel
    {
        public TransportationType Type { get; set; }

        public uint Capacity { get; set; }

        public ICollection<Container> LoadContainers { get; set; } = new HashSet<Container>();
    }
}
