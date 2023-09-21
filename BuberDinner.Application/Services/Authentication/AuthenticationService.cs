using System;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Application.Persistence;
using BuberDinner.Domain.Entities;
public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
      public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
         //1. Validate the user doesn't already exist
         if(_userRepository.GetUserByEmail(email) != null)
         {
            throw new Exception("UUser with given email already exists");
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

    public AuthenticationResult Login(string email, string password)
    {
       //1. Validate user exists
        if(_userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("User with given email does not exist");
        }
       //2. Validate password is correct
        if(user.Password != password)
        {
            throw new Exception("Password is incorrect");
        }

       //3. Create JWT token

       var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }


}