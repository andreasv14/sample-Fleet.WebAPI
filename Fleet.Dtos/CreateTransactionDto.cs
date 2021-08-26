namespace Fleet.Dtos
{
    public class CreateTransactionDto
    {
        public int FromTransportationId { get; set; }

        public int ToTransportationId { get; set; }
    }
}
