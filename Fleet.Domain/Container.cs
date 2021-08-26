namespace Fleet.Domain
{
    public class Container : BaseModel
    {
        public int? TransportationId { get; set; }
        public Transportation Transportation { get; set; }
    }
}
