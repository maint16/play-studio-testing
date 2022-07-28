using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using quest_api.Models;
using quest_api.Services;
using quest_api.ViewModels.Requests;
using quest_api.ViewModels.Responses;
using quest_entity;
using quest_entity.Models;

namespace quest_api_tests
{
    [TestClass]
    public class PlayerServiceTests
    {
        [TestMethod]
        [DataRow(1,1,20,2,2,8)]
        [DataRow(2,2,28,2,2,16)]
        [DataRow(3,5,46,7,3,34)]
        public async Task Process_PassValidData_ShouldReturn200(int playerLevel, int chipAmountBet,
            int totalQuestPercentCompleted, int chipsAwarded, int milestoneIndex, int questPointsEarned)
        {
            // arrange
            var dbContectMock = new Mock<QuestContext>();
            var player = new Player { Id = Guid.NewGuid(), EarnedPoints = 12, LastMilestoneIndexCompleted = 1 };
            dbContectMock.Setup(c => c.FindAsync<Player>(It.IsAny<Guid>()))
                .ReturnsAsync(player);
            var config = new QuestConfiguration();
            config.RateFromBet = 5;
            config.LevelBonusRate = 3;
            config.TotalPoint = 100;
            config.Milestones = new MilestoneConfiguration[]{
                new() { ChipsAwarded=0, EarnedPoints=0},
                 new() { ChipsAwarded=1, EarnedPoints=10},
                  new() { ChipsAwarded=2, EarnedPoints=20},
                   new() { ChipsAwarded=5, EarnedPoints=30},
                   new() { ChipsAwarded=50, EarnedPoints=100}
            };
            var data = new ProgressSubmit()
            {
                PlayerId = Guid.NewGuid(),
                PlayerLevel = playerLevel,
                ChipAmountBet = chipAmountBet
            };
            var playerService = new PlayerService(dbContectMock.Object, config);

            // act
            var result = await playerService.CheckProgressAsync(data, It.IsAny<CancellationToken>());

            // assert
            Assert.AreEqual(totalQuestPercentCompleted, result.TotalQuestPercentCompleted);
            Assert.IsNotNull(result.MilestonesCompleted);
            Assert.AreEqual(chipsAwarded, result.MilestonesCompleted.ChipsAwarded);
            Assert.AreEqual(milestoneIndex, result.MilestonesCompleted.MilestoneIndex);
            Assert.AreEqual(questPointsEarned, result.QuestPointsEarned);
           
        }
    }
}