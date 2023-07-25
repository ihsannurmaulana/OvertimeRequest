using System.Security.Claims;

namespace API.Contracts;

public interface ITokenHandler
{
    string GenerateToken(IEnumerable<Claim> claims);
}
