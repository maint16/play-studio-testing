using quest_api.ViewModels.Requests;
using quest_api.ViewModels.Responses;

namespace quest_api.Services
{
    public interface IPlayerService
    {
        Task<ProgressResponse> CheckProgressAsync(ProgressSubmit data, CancellationToken cancellation = default);
        Task<GetStateResponse> GetStateAsync(Guid playerId, CancellationToken cancellation = default);
    }
}
