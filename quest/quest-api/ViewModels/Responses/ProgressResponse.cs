namespace quest_api.ViewModels.Responses
{
    public class ProgressResponse
    {
        public int QuestPointsEarned { get; set; }
        public int TotalQuestPercentCompleted { get; set; }
        public MilestoneCompletion? MilestonesCompleted { get; set; }
    }
}
