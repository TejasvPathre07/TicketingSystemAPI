using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TicketingSystem.Models;

namespace TicketingSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<TicketHistory> TicketHistories { get; set; }
    }
}