using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Shared.Enums;

namespace BackendTask.Domain.Models.BankCards
{
    public class BankCardDomain
    {
        public long Id { get; set; }

        public string CardNumber { get; set; }

        public DateTime ExpirationDate { get; set; }

        public CardType CardType { get; set; }

        public string IssuingBank { get; set; }

        public string AccountNumber { get; set; }

        public long UserId { get; set; }

        public string Url { get; set; }
    }
}
