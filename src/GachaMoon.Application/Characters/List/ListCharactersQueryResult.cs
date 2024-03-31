using GachaMoon.Application.Characters.Common;

namespace GachaMoon.Application.Characters.List;

public record ListCharactersQueryResult(ICollection<CharacterData> Characters)
{
}
