using CleanArchitecture.Abstractions;

namespace CleanArchitecture.Services
{
    public interface ITokenHanldeService
    {
        Task<string> GenerateJwtToken(ITokenParameters parms);
    }
}
