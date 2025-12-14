using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Application.Common.Interfaces;

public interface ITokenService
{
    string GenerateToken(ApplicationUser user, IList<string> roles);
}
