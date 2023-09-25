using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Persistence;
using BuberDinner.Domain.Entities;
using ErrorOr;
using BuberDinner.Domain.Errors;
using BuberDinner.Application.Services.Commons;

namespace BuberDinner.Application.Services.Authentication.Queries
{

    public class AuthenticationQueryService : IAuthenticationQueryService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;

        }
        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            //1. Validate user exists
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }
            //2. Validate password is correct
            if (user.Password != password)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            //3. Create JWT token

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }

    }
}