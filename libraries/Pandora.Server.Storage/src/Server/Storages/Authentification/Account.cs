using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pandora.Server.Storages.Authentification
{
    [Table("accounts")]
    public sealed class Account
    {
        [Key]
        [Column("account_id")]
        public long Id { get; set; }

        [Column("account_name")]
        public string Name { get; set; } = "";

        [Column("account_mail")]
        public string Mail { get; set; } = "";

        [Column("account_password_salt")]
        public string Salt { get; set; } = "";

        [Column("account_password_hash")]
        public string PasswordHash { get; set; } = "";

        [Column("account_create_date")]
        public DateTime CreateDate { get; set; }

        [Column("account_change_date")]
        public DateTime ChangeDate { get; set; }

    }
}
