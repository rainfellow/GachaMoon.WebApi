using MediatR;

namespace GachaMoon.WebApi.Endpoints.Admin.Projects;

public class UserController : AdminControllerBase
{
    public UserController(ISender sender) : base(sender)
    {
    }
}
