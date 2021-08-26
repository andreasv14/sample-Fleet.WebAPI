using System.Collections.Generic;

namespace Fleet.Models
{
    public class Container : BaseModel
    {
        public int? TransportationId { get; set; }
        public Transportation Transportation { get; set; }
    }
}
