using Ensersoft.Robots;
using GachaMoon.Database;
using GachaMoon.Services.Abstractions.Clients;
using GachaMoon.Services.Abstractions.Time;
using Microsoft.EntityFrameworkCore;
using GachaMoon.Domain.Animes;
using DetectLanguage;
using System.Text.RegularExpressions;
using WanaKanaShaapu;
using GachaMoon.Common.Query;

namespace GachaMoon.Robots.AnimeAliasScrapper;

public class AnimeAliasScrapperRobot(
    IAnimeClient animeClient,
    IClockProvider clockProvider,
    ApplicationDbContext dbContext,
    ILogger<AnimeAliasScrapperRobot> logger) : IRobot
{
    private static readonly Regex NonLatinLettersRegex = new(@"\p{IsCJKUnifiedIdeographs}|\p{IsCyrillic}|\p{IsArabic}|\p{IsHebrew}|\p{IsThai}|\p{IsHiragana}|\p{IsKatakana}");
    private static readonly Regex KanaRegex = new(@"\p{IsHiragana}|\p{IsKatakana}");
    private static readonly Regex ThaiRegex = new(@"\p{IsThai}");
    private static readonly Regex LatinLettersRegex = new(@"[\p{IsBasicLatin}-[\s]]");
    private readonly IAnimeClient _animeClient = animeClient;
    private readonly IClockProvider _clockProvider = clockProvider;
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<AnimeAliasScrapperRobot> _logger = logger;
    private readonly DetectLanguageClient _client = new("c116628f573388ef87a35ddb61a12128");

    private readonly List<string> _badAliasesList = new() { "Fleet Girls Collection", "Kinos Journey", "KnT", "Kino`s Journey", "Kino's Journey : The Beautiful World - The Animated Series",
        "Kino’s Journey: The Beautiful World - The Animated Series", "Kino`s Journey: The Beautiful World - The Animated Series", "Kino`s Journey (2017)", "Kino`s Journey -the Beautiful World- the Animated Series", "kino2",
        "Kino’s Travels: The Beautiful World", "Komori-san Can`t Decline!", "Komori-san Can’t Decline!", "komisan", "Komi-san wa", "המשאלה של קומי", "Konosuba : Une explosion dans ce monde merveilleux !",
        "Yurucamp", "Yurukyan△", "Yurukyan", "BUILD-DIVIDE -#FFFFFF- CODE WHITE", "Build Divide -#FFFFFF- Code White", "Build-Divide : #ffffff Code White", "Build Divide: #FFFFFF - Code White", "Build Divide: #000000 2nd Season",
        "BUILD DIVIDE -#FFFFFF-", "Build Divide: Code Black 2nd Season", "Fruba", "Furuba", "Fruits Basket", "水果篮子（第一季）",
        "JoJo's Bizarre Adventure Part 3", "Le bizzarre avventure di JoJo: Stardust Crusaders", "Dai San Bu Kujo Jotaro: Mirai e no Isan", "Невероятное приключение ДжоДжо: Рыцари звёздной пыли", "jojo3", "jojo s3", "ההרפתקה המוזרה של ג`וג`ו: צלבני אבק הכוכבים - הקרב במצרים",
        "جوجو 2", "Martial Universe", "Megaton Musashi", "Mob Psycho Hyaku", "Mob Psycho One Hundred", "Mix", "mix", "MIX: Meisei Story", "بنها", "僕のヒーローアカデミア", "أكاديميتي للأبطال", "ワールドトリガー", "魔入りました！入間くん",
        "ビルディバイド -#000000(コードブラック)-", "Mahoyome", "まちカドまぞく", "Sekai Meisaku Gekijou", "WMT", "TenTen", "tenten s1", "YakuNeba", "約束のネバーランド", "神印王座 (2022)", "Shen Yin Wangzuo (2022)", "Shen Yin Wang Zuo (2022)",
        "トニカクカワイイ", "転生したらスライムだった件"};
    public async Task RunJob()
    {
        await ProcessDbAnimes();
        //await UpdateAnimeEpisodes();
    }

    private async Task ProcessDbAnimes()
    {
        var errorCount = 0;
        var now = _clockProvider.Now;
        var animesToUpdate = await _dbContext.Animes
            .Include(x => x.AnimeAliases)
            .IsNotDeleted()
            .Where(x => !x.AnimeAliases.Any())
            .Select(x => new { x.Id, x.AnimeBaseId })
            .ToListAsync();

        foreach (var anime in animesToUpdate)
        {
            if (anime.Id is 416 or 612 or 642 or 683)
            {
                continue;
            }
            try
            {
                //Select(x => x.Replace('！', '!').Replace('？', '?').Replace('：', ':')).
                var animeData = await _animeClient.AnimeFromId(anime.AnimeBaseId);

                var aliasList = animeData.Synonyms.Distinct().Where(x => x.Length > 2).Where(x => !_badAliasesList.Contains(x)).Select(async x => new AnimeAlias
                {
                    AnimeId = anime.Id,
                    Alias = x,
                    Language = await DetectAliasLanguage(x),
                    CreatedAt = _clockProvider.Now
                }).ToList();
                var a = await Task.WhenAll(aliasList);
                await _dbContext.AnimeAliases.AddRangeAsync(a);
                await _dbContext.SaveChangesAsync();

                Thread.Sleep(200);
            }
            catch (Exception ex)
            {
                errorCount++;
                if (errorCount > 100)
                {
                    break;
                }
                Thread.Sleep(5000);
            }
        }
    }

    private async Task UpdateAnimeEpisodes()
    {
        var now = _clockProvider.Now;
        // var animesToUpdate = await _dbContext.AnimeAliases
        //     .Where(x => x.Language == "ERR")
        //     .ToListAsync();

        // var batchCounter = 0;
        // var batchSize = 200;

        // foreach (var anime in animesToUpdate)
        // {
        //     anime.Language = await DetectAliasLanguage(anime.Alias);
        //     anime.UpdatedAt = now;
        //     if (++batchCounter % batchSize == 0)
        //     {
        //         await _dbContext.SaveChangesAsync();
        //     }
        // }
        // await _dbContext.SaveChangesAsync();
        var emptyEpisodes = await _dbContext.AnimeEpisodes
            .Include(x => x.AnimeImages)
            .IsNotDeleted()
            .Where(x => !x.AnimeImages.Any())
            .ToListAsync();

        foreach (var episode in emptyEpisodes)
        {
            episode.DeletedAt = now;
        }

        await _dbContext.SaveChangesAsync();

        var animesToUpdate = await _dbContext.Animes
            .Include(x => x.AnimeEpisodes)
            .ToListAsync();
        foreach (var anime in animesToUpdate)
        {
            var index = 1;
            foreach (var episode in anime.AnimeEpisodes)
            {
                if (episode.DeletedAt != null)
                {
                    continue;
                }
                episode.EpisodeNumber = index++;
                episode.UpdatedAt = now;
            }
            await _dbContext.SaveChangesAsync();
        }
    }

    private async Task<string> DetectAliasLanguage(string alias)
    {
        var rnd = new Random();
        await Task.Delay(rnd.Next(50, 400));
        var hasNonLatinLetters = alias.Any((x) => NonLatinLettersRegex.IsMatch(x.ToString()));
        var hasKana = alias.Any((x) => KanaRegex.IsMatch(x.ToString()));
        var hasThai = alias.Any((x) => ThaiRegex.IsMatch(x.ToString()));
        if (hasKana)
        {

            //_logger.LogInformation("{Alias} has kana. Setting as Japanese", alias);
            return "ja";
        }
        if (hasThai)
        {

            //_logger.LogInformation("{Alias} has thai.", alias);
            return "th";
        }

        var aliasUpd = new string(alias);

        if (hasNonLatinLetters)
        {
            aliasUpd = LatinLettersRegex.Replace(alias, string.Empty);
        }

        var code = await _client.DetectCodeAsync(aliasUpd);

        var isRomaji = IsRomaji(aliasUpd);
        if (isRomaji)
        {
            if (code is not "en" and not "es" and not "fr" and not "de" and not "it" and not "pt")
            {
                code = "rmj";
            }
            else
            {
                _logger.LogInformation("{Alias} detected as romaji but has language {Language}", alias, code);
                if (code is not "en")
                {
                    code = "rmj";
                }
            }
        }
        if (code == null)
        {
            return "rmj";
        }
        if (code.Length > 3)
        {
            code = code[..2];
        }
        if (code is "no" or "cs" or "et" or "sk" or "sr")
        {
            code = "rmj";
        }
        if (code is "da" or "sa")
        {
            code = "en";
        }
        // if (code is "bg")
        // {
        //     code = "ru";
        // }
        _logger.LogInformation("{Alias} detected as {Language}", alias, code);
        return code;
    }

    private bool IsRomaji(string text)
    {
        var totalUsefulTokens = 0;
        var totalGoodTokens = 0;
        var tokens = WanaKana.Tokenize(text, true);
        foreach (var token in tokens.Tokens)
        {
            if (token.Value.Length > 1 && token.Type == "en")
            {
                totalUsefulTokens++;
                var r = WanaKana.ToKana(token.Value);
                if (!WanaKana.IsMixed(r) && WanaKana.IsKana(r))
                {
                    totalGoodTokens++;
                }
            }
        }
        if (totalUsefulTokens == 0)
        {
            //_logger.LogInformation("No useful tokens: {Text}", text);
            return false;
        }
        if (totalGoodTokens > 2 || totalGoodTokens == totalUsefulTokens || (totalGoodTokens > 0 && totalUsefulTokens == 2))
        {
            //_logger.LogInformation("Detected as romaji: {Text}, converted: {TextJp}", text, WanaKana.ToKana(text));
            return true;
        }
        return false;
    }
}