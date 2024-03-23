using GachaMoon.Services.Abstractions.Database;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Database;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly ApplicationDbContext _context;

    public DatabaseInitializer(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Initialize()
    {
        _context.Database.Migrate();
    }
}
