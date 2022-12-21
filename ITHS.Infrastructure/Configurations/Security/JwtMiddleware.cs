using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ITHS.Infrastructure.Configurations.Security;

public class JwtMiddleware {
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next) {
        _next = next;
    }

    public async Task Invoke(HttpContext context) {
        string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token == null) {
            return;
        }

        int? userId = ValidateToken(token);

        await _next(context);
    }

    public static int? ValidateToken(string token) {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes("ITHSsuperSecretKey");

        try {
            tokenHandler.ValidateToken(token, new TokenValidationParameters {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
            int userId = int.Parse(jwtToken.Claims.First((x) => x.Type == "id").Value);

            return userId;
        } catch {
            return null;
        }
    }
}
