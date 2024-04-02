using GachaMoon.Services.Abstractions.Database;
using Microsoft.EntityFrameworkCore;

namespace GachaMoon.Database;

public class DatabaseInitializer(ApplicationDbContext context) : IDatabaseInitializer
{
    private readonly ApplicationDbContext _context = context;

    public void Initialize()
    {
        _context.Database.Migrate();
    }
}
