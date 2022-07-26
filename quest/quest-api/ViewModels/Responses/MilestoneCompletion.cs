namespace quest_api.ViewModels.Responses
{
    public class MilestoneCompletion
    {
        public MilestoneCompletion(int milestoneIndex, int chipsAwarded)
        {
            MilestoneIndex = milestoneIndex;
            ChipsAwarded = chipsAwarded;
        }

        public int MilestoneIndex { get; set; }
        public int ChipsAwarded { get; set; }
    }
}
