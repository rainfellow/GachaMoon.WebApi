using System.Text.Json.Serialization;

namespace GachaMoon.Clients.AnisongDB.Data;

public class AnisongDBRequest
{
    [JsonPropertyName("and_logic")]
    public bool AndLogic { get; set; } = false;

    [JsonPropertyName("anime_search_filter")]
    public AnisongAnimeSearchFilter AnimeSearchFilter { get; set; } = default!;

    [JsonPropertyName("chanting")]
    public bool Chanting { get; set; } = true;

    [JsonPropertyName("character")]
    public bool Character { get; set; } = true;

    [JsonPropertyName("dub")]
    public bool Dub { get; set; } = true;

    [JsonPropertyName("ending_filter")]
    public bool EndingFilter { get; set; } = true;

    [JsonPropertyName("ignore_duplicate")]
    public bool IgnoreDuplicate { get; set; } = true;

    [JsonPropertyName("insert_filter")]
    public bool InsertFilter = true;

    [JsonPropertyName("instrumental")]
    public bool Instrumental = true;

    [JsonPropertyName("normal_broadcast")]
    public bool NormalBroadcast = true;

    [JsonPropertyName("opening_filter")]
    public bool OpeningFilter = true;

    [JsonPropertyName("rebroadcast")]
    public bool Rebroadcast { get; set; } = true;

    [JsonPropertyName("standard")]
    public bool Standard { get; set; } = true;

    public AnisongDBRequest(string amqAnimeName)
    {
        AnimeSearchFilter = new AnisongAnimeSearchFilter
        {
            Search = amqAnimeName
        };
    }
}

public class AnisongAnimeSearchFilter
{
    [JsonPropertyName("partial_match")]
    public bool PartialMatch { get; set; } = false;

    [JsonPropertyName("search")]
    public string Search { get; set; } = default!;
}