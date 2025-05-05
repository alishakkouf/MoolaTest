using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTask.Domain.Models.Transactions
{
    public class CreateTransactionDomain
    {
        public decimal Amount { get; set; }

        public decimal Fee { get; set; }

        public long BankCardId { get; set; }
    }
}
