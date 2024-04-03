using System.Security.Claims;

namespace ApiCustomer.Security;

public interface ISecurityService
{
    string IsValidUser(IEnumerable<Claim> claims);
    string GenerateJwtToken(string username, string password);
}
