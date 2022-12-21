using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITHS.Infrastructure.Configurations.Security;

public class Authentication {
    public static string CreateJWTBearerToken(string serverHostUrl) {
        SymmetricSecurityKey key = GetSymmetricSecurityKey();
        SigningCredentials signinCredentials = DoDigitalSigningUsingTheKey(key);
        JwtSecurityToken jwtSecurityToken = CreateJwtSecurityToken(signinCredentials, serverHostUrl);

        string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return token;
    }

    private static SymmetricSecurityKey GetSymmetricSecurityKey() {
        string myKeyInConfig = "ITHSsuperSecretKey";

        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(myKeyInConfig));
    }

    private static SigningCredentials DoDigitalSigningUsingTheKey(SymmetricSecurityKey key) {
        SigningCredentials signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        return signinCredentials;
    }

    private static List<Claim> GetClaims() {
        return new List<Claim> {
            new Claim("School", "ITHS"),
            new Claim("Teacher", "Dawid")
        };
    }

    public static JwtSecurityToken CreateJwtSecurityToken(SigningCredentials signinCredentials, string serverHostUrl) {
        string issuerFromConfig = "ITHS";
        DateTime expireDate = DateTime.Now.AddMinutes(5);
        List<Claim> claims = GetClaims();

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: issuerFromConfig,
            audience: serverHostUrl,
            claims: claims,
            expires: expireDate,
            signingCredentials: signinCredentials
        );

        return jwtSecurityToken;
    }
}
