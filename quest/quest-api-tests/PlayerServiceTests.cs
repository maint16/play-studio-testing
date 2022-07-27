using Moq;
using quest_api.Models;
using quest_api.Services;
using quest_entity;
using quest_entity.Models;

namespace quest_api_tests
{
    [TestClass]
    public class PlayerServiceTests
    {
        [TestMethod]
        public void Process_PassValidData_ShouldReturn200()
        {
            var dbContectMock = new Mock<QuestContext>();
            var player = new Player { Id = Guid.NewGuid(), EarnedPoints = 15, LastMilestoneIndexCompleted = 1 };
            dbContectMock.Setup(c => c.FindAsync<Player>(It.IsAny<int>()))
                .ReturnsAsync(player);
            var config = new QuestConfiguration();
            config.RateFromBet = 5;
            config.LevelBonusRate = 3;
            config.TotalPoint = 100;
            config.Milestones = new MilestoneConfiguration[]{
                new MilestoneConfiguration { ChipsAwarded=0, EarnedPoints=0},
                 new MilestoneConfiguration { ChipsAwarded=1, EarnedPoints=10},
                  new MilestoneConfiguration { ChipsAwarded=2, EarnedPoints=20},
                   new MilestoneConfiguration { ChipsAwarded=5, EarnedPoints=100}
            };
            var playerService = new PlayerService(dbContectMock.Object, config);
            var result = playerService.CheckProgressAsync();
        }
    }
}