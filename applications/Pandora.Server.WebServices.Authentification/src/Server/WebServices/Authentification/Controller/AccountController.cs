using Microsoft.AspNetCore.Mvc;
using Pandora.Server.WebServices.Authentification.Models;
using Pandora.Server.WebServices.Authentification.Server.WebServices.Authentification.Services;

namespace Pandora.Server.WebServices.Authentification.Server.WebServices.Authentification.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountCreateService accountCreateService;

        public AccountController(IAccountCreateService accountCreateService)
        {
            this.accountCreateService = accountCreateService;
        }

        [HttpPost]
        public IActionResult CreateAccount([FromBody] AccountPostDto accountPostDto)
        {
            if (accountPostDto == null)
            {
                throw new BadRequestException((int)AuthentificationErrorCodes.InvalidDataPackage, "Empty message body");
            }

            var account = accountCreateService.Create(accountPostDto.Accountname, accountPostDto.AccountMail, accountPostDto.Password);

            return Ok(new AccountGetDto()
            {
                Accountname = account.Name,
                AccountMail = account.Mail,
            });
        }
    }
}
