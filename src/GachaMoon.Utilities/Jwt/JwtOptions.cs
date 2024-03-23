using System.ComponentModel.DataAnnotations;

namespace GachaMoon.Utilities.Jwt;

public record JwtOptions
{
    [Required]
    public string Key { get; set; } = default!;
    [Required]
    public string Issuer { get; set; } = default!;
    [Required]
    public string Audience { get; set; } = default!;
}
