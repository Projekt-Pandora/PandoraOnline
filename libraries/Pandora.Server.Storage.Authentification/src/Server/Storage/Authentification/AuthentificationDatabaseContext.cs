using Microsoft.EntityFrameworkCore;
using Pandora.Server.Storage.Authentification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Server.Storage.Authentification
{
    public class AuthentificationDatabaseContext : DbContext
    {
        public AuthentificationDatabaseContext(DbContextOptions<AuthentificationDatabaseContext> options) : base(options)
        { }

        public DbSet<AccountEntity> Accounts { get; private set; }
    }
}
