using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Shared.Enums;

namespace BackendTask.Domain.Models.BankCards
{
    public class CreateBankCardDomain
    {
        public string CardNumber { get; set; }

        public DateTime ExpirationDate { get; set; }

        public CardType CardType { get; set; }

        public string IssuingBank { get; set; }

        public string AccountNumber { get; set; }

        public long UserId { get; set; }

        public long? AttachmentId { get; set; }
    }
}
