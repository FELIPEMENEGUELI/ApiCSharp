using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.DTOs.Validations;
using MP.ApiDotNet6.Application.Services.Interfaces;
using MP.ApiDotNet6.Domain.Authentication;
using MP.ApiDotNet6.Domain.Repositories;

namespace MP.ApiDotNet6.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;   
        private readonly ITokenGenerator _tokenGenerator;

        public UserServices(IUserRepository userRepository, ITokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<ResultServices<dynamic>> GenerateTokenAsync(UserDTO userDTO)
        {
            if (userDTO == null)
                return ResultServices.Fail<dynamic>("Objeto deve ser informado");

            var validator = new UserDTOValidator().Validate(userDTO);
            if (!validator.IsValid)
                return ResultServices.RequestError<dynamic>("Problemas com a validação", validator);

            var user = await _userRepository.GetUserByEmailAndPasswordAsync(userDTO.Email, userDTO.Password);
            if (user == null)
                return ResultServices.Fail<dynamic>("Usuario ou senha nao encontrado");

            return ResultServices.Ok(_tokenGenerator.Generator(user));
        }
    }
}
