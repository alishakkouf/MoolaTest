using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Shared;
using Microsoft.AspNetCore.Identity;

namespace BackendTask.Data.Models
{
    public class Transaction : AuditedEntity, IEntity<long>
    {
        public long Id { get; set; }

        public decimal Amount { get; set; }

        public decimal Fee { get; set; }

        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

        public long BankCardId { get; set; }

        [ForeignKey(nameof(BankCardId))]
        public virtual BankCard BankCard { get; set; }
    }
}
