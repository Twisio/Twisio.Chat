using Microsoft.IdentityModel.Tokens;
using System.Text;

public class AuthenticationOptions
{
    public static string ISSUER = "https://localhost:5000";
    public static string AUDIENCE = "PortfolioWebAPI";
    const string KEY = "375ff8a55aca72cf3a11e318d1592d2f0d3995ae";
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}