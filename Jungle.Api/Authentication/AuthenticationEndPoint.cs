using Carter;
using Jungle.Shared.Requests;

namespace Jungle.Api.Authentication
{
    public class AuthenticationEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/authenticate", (UserInfoDto userInfo) =>
            {
                var token = JwtTokenGenerator.Generate(userInfo);

                return token;
            })
                .WithTags("Api Authentication");
        }
    }
}
