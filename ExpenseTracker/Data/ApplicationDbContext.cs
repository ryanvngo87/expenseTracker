using ExpenseTracker.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace ExpenseTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Expense> Expense { get; set; }
    }
}
