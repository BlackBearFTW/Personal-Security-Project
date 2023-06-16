using DigiChoiceBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace DigiChoiceBackend.Persistance;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Party> Parties { get; set; }
    public DbSet<PartyMember> PartyMembers { get; set; }
    public DbSet<Voter> Voters { get; set; }
}