using MediatR;

namespace GachaMoon.WebApi.Endpoints.Application.Test;

public class TestController(ISender sender) : ApiControllerBase(sender)
{
    // [AllowAnonymous]
    // [HttpGet("animeapi")]
    // public async Task<ActionResult<TestAnimeApiQueryResult>> CheckAnswer(
    //     [FromQuery] string query,
    //     CancellationToken cancellationToken)
    // {
    //     var command = new TestAnimeApiQuery(query);
    //     var result = await Sender.Send(command, cancellationToken);
    //     return Ok(result);
    // }

    // [AllowAnonymous]
    // [HttpGet("animelistapi/anime")]
    // public async Task<ActionResult<TestUserAnimeListApiQueryResult>> FindAnime(
    //     [FromQuery] string query,
    //     CancellationToken cancellationToken)
    // {
    //     var command = new TestUserAnimeListApiQuery(query);
    //     var result = await Sender.Send(command, cancellationToken);
    //     return Ok(result);
    // }

    // [AllowAnonymous]
    // [HttpGet("animelistapi/userlist")]
    // public async Task<ActionResult<GetUserAnimeListQueryResult>> GetUserAnime(
    //     [FromQuery] string user,
    //     CancellationToken cancellationToken)
    // {
    //     var command = new GetUserAnimeListQuery(user);
    //     var result = await Sender.Send(command, cancellationToken);
    //     return Ok(result);
    // }
}
