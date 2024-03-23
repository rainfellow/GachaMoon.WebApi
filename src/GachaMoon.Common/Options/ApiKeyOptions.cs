using System.ComponentModel.DataAnnotations;

namespace GachaMoon.Common.Options;

public class ApiKeyOptions
{
    public const string ApiKeyHeader = "X-Api-Key";

    [Required]
    public string ApiKey { get; set; }
}