using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiCustomer.Security;

public class SecurityService : ISecurityService
{
    public string GenerateJwtToken(string username, string password)
    {
      

        // Replace "your-secret-key" with your actual secret key

        // Convert the secret key to a byte array
        byte[] keyBytes = Encoding.UTF8.GetBytes(SecurityInfo.SecretKey);

        // Create a SymmetricSecurityKey using the byte array
        var key = new SymmetricSecurityKey(keyBytes);

        //HS256
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "codehub.gr",
            audience: "all-our-trainees",
            claims: [new Claim(ClaimTypes.Name, username), new Claim(ClaimTypes.Country, "Greece") ],
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string IsValidUser(IEnumerable<Claim> claims)
    {
        throw new NotImplementedException();
    }
}
