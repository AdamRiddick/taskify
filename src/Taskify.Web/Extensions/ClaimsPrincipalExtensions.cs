namespace Taskify.Web.Extensions;

using System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        return Convert.ToInt32(userId);
    }

    public static bool HasScope(this ClaimsPrincipal claimsPrincipal, string scope)
    {
        var claim = claimsPrincipal
                        .FindFirst(
                            x => x.Type.Equals("scope", StringComparison.InvariantCultureIgnoreCase)
                              && x.Value.Equals(scope, StringComparison.InvariantCultureIgnoreCase));
        return claim != null;
    }
}
