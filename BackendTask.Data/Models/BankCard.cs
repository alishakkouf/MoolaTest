using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Net.Mail;
using BackendTask.Data.Models.Identity;
using BackendTask.Shared;
using BackendTask.Shared.Enums;

namespace BackendTask.Data.Models
{
    public class BankCard : AuditedEntity, IEntity<long>
    {
        public long Id { get; set; }

        [StringLength(100)]
        public required string CardNumber { get; set; }

        public required DateTime ExpirationDate { get; set; }

        public required CardType CardType { get; set; }

        [StringLength(100)]
        public required string IssuingBank { get; set; }

        [StringLength(20)]
        public required string AccountNumber { get; set; }

        public long UserId { get; set; }

        public long? AttachmentId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual UserAccount User { get; set; }

        [ForeignKey(nameof(AttachmentId))]
        public virtual Attachment Attachment { get; set; }
    }   
}
