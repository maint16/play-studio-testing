namespace quest_api.Models
{
    public class QuestConfiguration
    { 
        public int RateFromBet { get; set; }
        public int LevelBonusRate { get; set; }
        public int TotalPoint { get; set; }
        public MilestoneConfiguration[] Milestones { get; set; }
    }
}
