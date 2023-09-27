
using BuberDinner.Application.Authentication.Commons;
using BuberDinner.Contracts.Authentication;
using Mapster;

namespace BuberDinner.Api.Commons.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Token, src => src.Token)
                .Map(dest => dest, src => src.User);
        }
    }
}