namespace Model
{
    public class Hack
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int BaseDifficulty { get; set; }
        public int SkillDifficulty { get; set; }
        public SkillCategory SkillCategory { get; set; }
        public int EnergyCost { get; set; }
        public int Reward { get; set; }
        public RewardType RewardType { get; set; }

        public Hack()
        {

        }
        public Hack(string name, string description, int baseDifficulty, int skillDifficulty, SkillCategory skillCategory, int energyCost, int reward, RewardType rewardType)
        {
            Name = name;
            Description = description;
            BaseDifficulty = baseDifficulty;
            SkillDifficulty = skillDifficulty;
            SkillCategory = skillCategory;
            EnergyCost = energyCost;
            Reward = reward;
            RewardType = rewardType;
        }

    }
}