using quest_api.Models;
using quest_api.ViewModels.Requests;
using quest_api.ViewModels.Responses;
using quest_entity;
using quest_entity.Models;

namespace quest_api.Services
{
    public class PlayerService : IPlayerService
    {
        private QuestContext _questContext;
        private QuestConfiguration _questConfiguration;
        public PlayerService(QuestContext questContext, QuestConfiguration questConfiguration)
        {
            _questContext = questContext;
            _questConfiguration = questConfiguration;
        }

        public async Task<ProgressResponse> CheckProgressAsync(ProgressSubmit data, CancellationToken cancellation = default)
        {
            var progressResult = new ProgressResponse();
            // Get player info.
            var player = await _questContext.FindAsync<Player>(data.PlayerId);
            if (player == null)
                throw new NullReferenceException("Player not found.");

            //“QuestPointsEarned”: (ChipAmountBet * RateFromBet) + (PlayerLevel * LevelBonusRate)
            progressResult.QuestPointsEarned = data.ChipAmountBet * _questConfiguration.RateFromBet + data.PlayerLevel * _questConfiguration.LevelBonusRate;
            // Check max points.
            var totalEarnedPoints = player.EarnedPoints + progressResult.QuestPointsEarned;
            if (totalEarnedPoints > _questConfiguration.TotalPoint)
                throw new Exception("The earned points exceeded the total points.");

            // Update and save earned points to db.
            player.EarnedPoints = totalEarnedPoints;
            await _questContext.SaveChangesAsync(cancellation);
            progressResult.TotalQuestPercentCompleted = (int)Math.Round((decimal)(totalEarnedPoints * 100 / _questConfiguration.TotalPoint), MidpointRounding.AwayFromZero);
            var newMilestone = _questConfiguration.Milestones.Where(c => c.EarnedPoints <= player.EarnedPoints).OrderBy(c => c.EarnedPoints).LastOrDefault();
           
            if (newMilestone == null)
            {
                // Player not pass the first milestone.
                return progressResult
            }
              
            // Check the number of milestone that player achieved.
            var newMilestoneIndex = Array.FindIndex(_questConfiguration.Milestones, c => c.EarnedPoints == newMilestone.EarnedPoints);

            if (newMilestoneIndex < player.LastMilestoneIndexCompleted)
                throw new Exception("A player cannot complete a milestone more than once.");

            // The new earned points not pass the new milestone.
            else if (newMilestoneIndex == player.LastMilestoneIndexCompleted)
                progressResult.MilestonesCompleted = new MilestoneCompletion(newMilestoneIndex, 0);

            else
            {
                // Sum all the chips from milestones that player archived.
                var chipsAwarded = 0;
                var fromIndex = player.LastMilestoneIndexCompleted == null ? 0 : player.LastMilestoneIndexCompleted + 1 ;
                for (int i = (int)fromIndex; i <= newMilestoneIndex; i++)
                    chipsAwarded = chipsAwarded + _questConfiguration.Milestones[i].ChipsAwarded;
                progressResult.MilestonesCompleted = new MilestoneCompletion(newMilestoneIndex, chipsAwarded);
            }

            return progressResult;
        }

        public async Task<GetStateResponse> GetStateAsync(Guid playerId, CancellationToken cancellation = default)
        {
            var getStateResult = new GetStateResponse();

            // Get player info.
            var player = await _questContext.FindAsync<Player>(playerId);
            if (player == null)
                throw new NullReferenceException("Player not found.");

            getStateResult.TotalQuestPercentCompleted = (int)Math.Round((decimal)(player.EarnedPoints / _questConfiguration.TotalPoint * 100));
            getStateResult.LastMilestoneIndexCompleted = player.LastMilestoneIndexCompleted.Value;

            return getStateResult;
        }
    }
}
