using ITHTD.Models;
using Microsoft.EntityFrameworkCore;

namespace ITHTD.Data;

public class HelpdeskContext : DbContext
{
    public HelpdeskContext(DbContextOptions<HelpdeskContext> options) : base(options) { }

    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Agent> Agents => Set<Agent>();
}