namespace Model
{
    public class Mission
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Reward { get; set; }
        public RewardType RewardType { get; set; }

        public Mission()
        {

        }
        public Mission(string name, string description, int reward, RewardType rewardType)
        {
            Name = name;
            Description = description;
            Reward = reward;
            RewardType = rewardType;
        }
    }
}