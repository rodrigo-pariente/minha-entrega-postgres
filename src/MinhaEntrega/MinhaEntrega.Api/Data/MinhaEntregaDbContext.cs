using MinhaEntrega.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace MinhaEntrega.Api.Data;

public class MinhaEntregaContext(DbContextOptions<MinhaEntregaContext> options)
    : DbContext(options)
{
    // Qual a diferença com `public DbSet<Order> Orders => Set<Order>();`?
    public DbSet<Order> Orders { get; set; }

    public DbSet<Event> Events { get; set; }
}
