using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOurShop
{
    public class DatabaseFixture:IDisposable
    {
        public AdoNetManageContext Context { get; private set; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<AdoNetManageContext>()
                .UseSqlServer("Server=srv2\\pupils;Database=Test_OurShop;Trusted_Connection=True;TrustServerCertificate=True")
                .Options;
            Context = new AdoNetManageContext(options);
            Context.Database.EnsureCreated();

        }
        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
