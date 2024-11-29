using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Server.WebServices.Models.Authentification.DTO
{
    public record AccountGetDto
    (
        string AccountName,
        string AccountMail,
        AccountType AccountType,
        bool Enabled,
        bool Verified,
        DateTime CreateDate,
        DateTime ChangeDate,
        bool IsDeleted
    );
}
