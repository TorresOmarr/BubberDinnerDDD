using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Application.Persistence;
using BuberDinner.Domain.Entities;
using ErrorOr;
using BuberDinner.Domain.Errors;
using BuberDinner.Application.Services.Authentication.Commands;
using BuberDinner.Application.Services.Commons;
public class AuthenticationCommandService : IAuthenticationCommandService
{
   private readonly IJwtTokenGenerator _jwtTokenGenerator;
   private readonly IUserRepository _userRepository;
   public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
   {
      _jwtTokenGenerator = jwtTokenGenerator;
      _userRepository = userRepository;
   }
   public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
   {
      //1. Validate the user doesn't already exist
      if (_userRepository.GetUserByEmail(email) != null)
      {
         return Errors.User.DuplicateEmail;
      }



      //2. Create user (generate unique ID) & persist to BD
      var user = new User()
      {
         FirstName = firstName,
         LastName = lastName,
         Email = email,
         Password = password
      };

      _userRepository.Add(user);

      //3. Create JWT token'=

      var token = _jwtTokenGenerator.GenerateToken(user);

      return new AuthenticationResult(user, token);
   }



}