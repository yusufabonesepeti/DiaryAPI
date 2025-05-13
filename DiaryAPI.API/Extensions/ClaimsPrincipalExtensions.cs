using System.Security.Claims;

namespace DiaryAPI.API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        return user.FindFirst("Id")?.Value;
    }
}