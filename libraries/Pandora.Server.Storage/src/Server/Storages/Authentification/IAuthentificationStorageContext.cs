using Microsoft.EntityFrameworkCore;

namespace Pandora.Server.Storages.Authentification
{
    public interface IAuthentificationStorageContext : IDbContext
    {
        DbSet<Account> Accounts { get; }
    }
}