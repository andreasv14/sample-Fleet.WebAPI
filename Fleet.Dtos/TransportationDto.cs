using System.Collections.Generic;

namespace Fleet.Dtos
{
    public class TransportationDto
    {
        public int TransportationId { get; set; }

        public int Capacity { get; set; }

        public TransportationTypeDto Type { get; set; }

        public IEnumerable<ContainerDto> Containers { get; set; }
    }
}
