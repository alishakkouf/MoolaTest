using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTask.Domain.Models.Transactions
{
    public class TransactionDomain
    {
        public long Id { get; set; }

        public decimal Amount { get; set; }

        public decimal Fee { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public long BankCardId { get; set; }
    }
}
