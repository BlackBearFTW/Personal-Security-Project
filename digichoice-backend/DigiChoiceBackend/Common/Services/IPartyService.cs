using DigiChoiceBackend.Models;
using ErrorOr;

namespace DigiChoiceBackend.Common.Services;

public interface IPartyService
{
    public Task<ErrorOr<List<Party>>> GetAllParties(string? searchByName);
    public Task<ErrorOr<Party>> GetBySlug(string slug);
}