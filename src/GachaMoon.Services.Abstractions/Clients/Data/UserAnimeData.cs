namespace GachaMoon.Services.Abstractions.Clients.Data;
public record UserAnimeData
{
    public int Id { get; set; } = default!;
    public string Title { get; set; } = default!;
}