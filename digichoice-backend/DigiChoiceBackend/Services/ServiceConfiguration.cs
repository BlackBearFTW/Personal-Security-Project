using System.Diagnostics.CodeAnalysis;
using DigiChoiceBackend.Common.Services;

namespace DigiChoiceBackend.Services;

[ExcludeFromCodeCoverage]
public static class ServiceConfiguration
{
    public static IServiceCollection AddProjectServices(this IServiceCollection services)
    {
        services.AddScoped<IPartyService, PartyService>();
        services.AddScoped<IPartyMemberService, PartyMemberService>();
        services.AddScoped<IVoteService, VoteService>();

        return services;
    }
}