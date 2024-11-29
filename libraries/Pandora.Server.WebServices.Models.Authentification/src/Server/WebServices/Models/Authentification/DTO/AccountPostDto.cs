using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Server.WebServices.Models.Authentification.DTO
{
    public record AccountPostDto
    (
        string AccountName,
        string AccountMail,
        string Password
    );
}
