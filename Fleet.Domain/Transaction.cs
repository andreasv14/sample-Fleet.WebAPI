using Fleet.Domain.Enums;
using System;

namespace Fleet.Domain
{
    public class Transaction : BaseModel
    {
        public DateTime IssueDate { get; set; } = DateTime.Now;

        public TransactionType Type { get; set; }

        public int FromTransportationId { get; set; }
        public Transportation FromTransportation { get; set; }

        public int ToTransportationId { get; set; }
        public Transportation ToTransportation { get; set; }
    }
}
