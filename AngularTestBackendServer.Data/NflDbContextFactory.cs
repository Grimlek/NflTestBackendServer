using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AngularTestBackendServer.Data;

public class NflDbContextFactory : IDesignTimeDbContextFactory<NflDbContext>
{
    public NflDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<NflDbContext>();
        
        //optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=Nfl;Integrated Security=True");
        optionsBuilder.UseSqlServer("Server=tcp:csexton-test-db-server.database.windows.net,1433;Initial Catalog=csexton-test-db;Persist Security Info=False;User ID=csexton-admin;Password=63b44068-6a7d-4373-845a-da4e133d0732;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        
        return new NflDbContext(optionsBuilder.Options);
    }
}
