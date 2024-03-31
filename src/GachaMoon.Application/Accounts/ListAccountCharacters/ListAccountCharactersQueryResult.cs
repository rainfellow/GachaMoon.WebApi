using GachaMoon.Application.Accounts.Common;

namespace GachaMoon.Application.Accounts.ListAccountCharacters;

public record ListAccountCharactersQueryResult
{
    public string AccountName { get; set; } = default!;
    public ICollection<AccountCharacterData> AccountCharacters { get; set; } = default!;
}
