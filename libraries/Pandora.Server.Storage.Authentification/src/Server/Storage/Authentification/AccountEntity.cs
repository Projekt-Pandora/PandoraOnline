using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Server.Storage.Authentification
{
    [Table("accounts")]
    public class AccountEntity
    {
        [Column("account_id")]
        [Key]
        public long Id { get; set; }

        [Column("account_type")]
        public AccountType AccountType { get; set; }

        [Column("account_name")]
        public string Name { get; set; }

        [Column("account_email")]
        public string EMail { get; set; }

        [Column("account_passwordHash")]
        public string PasswordHash { get; set; }

        [Column("account_createDate")]
        public DateTime CreateDate { get; set; }

        [Column("account_changeDate")]
        public DateTime ChangeDate { get; set; }

        [Column("account_enabled")]
        public bool IsEnabled { get; set; }

        [Column("account_validated")]
        public bool IsValidated { get; set; }
    }
}
