using System.Collections.Generic;

namespace Fleet.Dtos
{
    public class LoadContainersDto
    {
        public int TransportationId { get; set; }

        public IEnumerable<int> ContainerIds { get; set; }
    }
}
