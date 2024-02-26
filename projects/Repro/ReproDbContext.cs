using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Repro.Models;

namespace Repro;

public class ReproDbContext : DbContext
{
    public ReproDbContext(DbContextOptions<ReproDbContext> options) : base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // because we're using multiple levels of owned types, our field names get super long.
        // field names longer than 63 characters get truncated by Postgres.
        // if the first 63 characters of two fields are the same, EF Core appends a number to deduplicate it;
        // however, this number is the 64th character of the name, which gets truncated away...
        // here we decrease the character limit for the generated names, so the appended number does not get eaten by postgres.
        configurationBuilder.Conventions.Add(serviceProvider => new RelationalMaxIdentifierLengthConvention(60,
            serviceProvider.GetRequiredService<ProviderConventionSetBuilderDependencies>(),
            serviceProvider.GetRequiredService<RelationalConventionSetBuilderDependencies>()
        ));
    }

    public DbSet<Staffing> Staffings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Staffing>(staffingBuilder =>
        {
            {
                // owned-1prop
                staffingBuilder.OwnsOne(a => a.Oven,
                    staffEntryBuilder =>
                    {
                        staffEntryBuilder.OwnsOne(se => se.Tasks);
                    });
                
                // owned-2prop
                // staffingBuilder.OwnsOne(a => a.Oven,
                //     staffEntryBuilder =>
                //     {
                //         staffEntryBuilder.OwnsOne(se => se.Tasks);
                //     });
                // staffingBuilder.OwnsOne(a => a.Bakery,
                //     staffEntryBuilder =>
                //     {
                //         staffEntryBuilder.OwnsOne(se => se.Tasks);
                //     });
                
                // complex-1prop
                // staffingBuilder.ComplexProperty(a => a.Oven,
                //     staffEntryBuilder =>
                //     {
                //         staffEntryBuilder.IsRequired();
                //         staffEntryBuilder.ComplexProperty(se => se.Tasks, seb => seb.IsRequired());
                //     });
            }
        });
    }
}