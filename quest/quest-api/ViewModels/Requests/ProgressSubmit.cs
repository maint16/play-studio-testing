namespace quest_api.ViewModels.Requests
{
    public class ProgressSubmit
    {
        public Guid PlayerId { get; set; }
        public int PlayerLevel { get; set; }
        public int ChipAmountBet { get; set; }
    }
}
