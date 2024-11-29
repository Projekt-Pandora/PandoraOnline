using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Server.Storage.Authentification.Models
{
    [Table("accounts")]
    public class AccountEntity
    {
        [Column("account_id")] public long AccountId { get; set; }
        [Column("account_name")] public string Name { get; set; } = string.Empty;
        [Column("account_mail")] public string EMail { get; set; } = string.Empty;
        [Column("account_password_hash")] public string PasswordHash { get; set; } = string.Empty;
        [Column("account_password_salt")] public string PasswordSalt { get; set; } = string.Empty;
        [Column("account_type")] public int AccountType { get; set; }
        [Column("account_enabled")] public bool Enabled { get; set; }
        [Column("account_verified")] public bool Verified { get; set; }
        [Column("account_createDate")] public DateTime CreateDate { get; set; }
        [Column("account_changeDate")] public DateTime ChangeDate { get; set; }
        [Column("account_deleteDate")] public DateTime? DeleteDate { get; set; }
    }
}
