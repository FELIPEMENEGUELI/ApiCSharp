using MP.ApiDotNet6.Application.DTOs;

namespace MP.ApiDotNet6.Application.Services.Interfaces
{
    public interface IUserServices
    {
        Task<ResultServices<dynamic>> GenerateTokenAsync(UserDTO userDTO);
    }
}
