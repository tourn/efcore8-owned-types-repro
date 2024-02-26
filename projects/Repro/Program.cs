using Microsoft.EntityFrameworkCore;
using Repro;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ReproDbContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql("Host=localhost; Database=postgres;Port=5433; User Id=postgres; Password=1234; Include Error Detail=true");
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{

    var acquisitionDbContext = scope.ServiceProvider.GetRequiredService<ReproDbContext>();

    acquisitionDbContext.Database.Migrate();

}

app.MapControllers();

app.Run();