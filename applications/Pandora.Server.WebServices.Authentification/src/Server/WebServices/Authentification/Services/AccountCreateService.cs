
using Pandora.Server.Storages.Authentification;

namespace Pandora.Server.WebServices.Authentification.Server.WebServices.Authentification.Services
{
    [Transient<IAccountCreateService>]
    public class AccountCreateService : IAccountCreateService
    {
        private const int SALT_LENGTH = 12;

        private readonly ILogger<AccountCreateService> logger;
        private readonly IAuthentificationStorageContext context;
        private readonly IPasswordService passwordService;

        public AccountCreateService(ILogger<AccountCreateService> logger, IAuthentificationStorageContext context, IPasswordService passwordService)
        {
            this.logger = logger;
            this.context = context;
            this.passwordService = passwordService;
        }

        public Account Create(string accountName, string accountMail, string plainPassword)
        {
            if (context.Accounts.Any(m => m.Name == accountName))
            {
                logger.LogError("Benutername {0} bereits vergeben", accountName);
                throw new UnprocessableEntityException((int)AuthentificationErrorCodes.AccountNameAlreadyExists, "Name already exists");
            }

            if (context.Accounts.Any(m => m.Mail == accountMail))
            {
                logger.LogError("EMail Adresse {0} bereits vergeben", accountMail);
                throw new UnprocessableEntityException((int)AuthentificationErrorCodes.AccountEMailAlreadyExists, "E-Mail already exists");
            }

            var salt = passwordService.GenerateSalt(SALT_LENGTH);
            var account = new Account()
            {
                Name = accountName,
                Mail = accountMail,
                Salt = salt,
                PasswordHash = passwordService.HashPassword(plainPassword, salt),
                CreateDate = DateTime.UtcNow,
                ChangeDate = DateTime.UtcNow,
            };

            context.Accounts.Add(account);
            context.SaveChanges();

            logger.LogInformation("Benutername {0} erstellt. AccountID: {1}", accountName, account.Id);

            return account;
        }
    }

    public interface IAccountCreateService
    {
        Account Create(string accountName, string accountMail, string plainPassword);
    }
}
