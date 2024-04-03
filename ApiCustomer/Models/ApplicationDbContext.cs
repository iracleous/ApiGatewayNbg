using ApiCustomer.Requests;
using Microsoft.EntityFrameworkCore;

namespace ApiCustomer.Models;

public class ApplicationDbContext: DbContext
{
    public DbSet<Credentials> Credentials { get; set; }
    public ApplicationDbContext( )
    {
         
    }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {

    }
}
