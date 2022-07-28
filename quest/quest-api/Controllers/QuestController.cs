using Microsoft.AspNetCore.Mvc;
using quest_api.Services;
using quest_api.ViewModels.Requests;

namespace quest_api.Controllers
{
    [ApiController]
    [Route("api")]
    public class QuestController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public QuestController(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        /// <summary>
        /// Get progress base on condition.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("progress")]
        public async Task<IActionResult> Progress([FromBody] ProgressSubmit command)
        {
            var result = await _playerService.CheckProgressAsync(command);
            return Ok(result);
        }

        /// <summary>
        /// Get the current state of player.
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [HttpPost("state/{playerId}")]
        public async Task<IActionResult> State([FromRoute] Guid playerId)
        {
            var result = await _playerService.GetStateAsync(playerId);
            return Ok(result);
        }
    }

  
}
