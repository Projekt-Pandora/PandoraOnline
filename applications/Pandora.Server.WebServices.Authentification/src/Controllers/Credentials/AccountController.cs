using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pandora.Server.Storage.Authentification;
using Pandora.Server.Storage.Authentification.Models;
using Pandora.Server.WebServices.Models.Authentification;
using Pandora.Server.WebServices.Models.Authentification.DTO;
using System.Security.Cryptography;

namespace Pandora.Controllers.Credentials
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IDbContextFactory<AuthentificationDatabaseContext> contextFactory) : ControllerBase
    {
        private static SHA256 __sha = SHA256.Create();

        [HttpPost]
        public IActionResult CreateAccount([FromBody] AccountPostDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Body is empty");
            }

            using var context = contextFactory.CreateDbContext();

            if (context.Accounts.Any(m => m.Name == dto.AccountName))
            {
                return UnprocessableEntity("Account name already exists");
            }

            if (context.Accounts.Any(m => m.EMail == dto.AccountMail))
            {
                return UnprocessableEntity("Account E-Mail already exists");
            }

            var accountEntity = new AccountEntity()
            {
                Name = dto.AccountName,
                EMail = dto.AccountMail,
                AccountType = (int)AccountType.Player,
                Enabled = true,
                Verified = false,
                CreateDate = DateTime.UtcNow,
                ChangeDate = DateTime.UtcNow,
                DeleteDate = null
            };

            SetAccountPassword(accountEntity, dto.Password);

            context.Accounts.Add(accountEntity);
            context.SaveChanges();

            return Ok(new AccountGetDto
            (
                accountEntity.Name,
                accountEntity.EMail,
                (AccountType)accountEntity.AccountType,
                accountEntity.Enabled,
                accountEntity.Verified,
                accountEntity.CreateDate,
                accountEntity.ChangeDate,
                accountEntity.DeleteDate.HasValue
            ));
        }



        private void SetAccountPassword(AccountEntity accountEntity, string password)
        {
            var salt = Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(accountEntity.Name));
            accountEntity.PasswordSalt = salt;

            var saltedPasswordByteArray = System.Text.Encoding.Unicode.GetBytes(password + salt);
            accountEntity.PasswordHash = Convert.ToBase64String(__sha.ComputeHash(saltedPasswordByteArray));
        }
    }
}
