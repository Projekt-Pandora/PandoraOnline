namespace Pandora.Server.Storages
{
    public interface IDbContext
    {
        int SaveChanges();
    }
}