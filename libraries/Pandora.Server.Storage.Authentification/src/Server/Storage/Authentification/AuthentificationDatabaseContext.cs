using Microsoft.EntityFrameworkCore;
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
    }
}
