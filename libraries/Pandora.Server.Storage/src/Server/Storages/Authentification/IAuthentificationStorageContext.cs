using Microsoft.EntityFrameworkCore;

namespace Pandora.Server.Storages.Authentification
{
    public interface IAuthentificationStorageContext
    {
        DbSet<Account> Accounts { get; }
    }
}