using Microsoft.EntityFrameworkCore;

namespace Rocky.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):
            base(options)
        {
            
        }
    }
}
