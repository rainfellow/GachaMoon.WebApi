using System.ComponentModel.DataAnnotations;

namespace GachaMoon.Common.Options;

public class AppHostOptions
{
    [Required]
    public string BasePath { get; set; } = default!;
}
