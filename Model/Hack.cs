namespace Model
{
    public class Hack
    {
        public int HackId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BaseDifficulty { get; set; }
        public int SkillDifficulty { get; set; }
        public int SkillCategoryId { get; set; }
        public int EnergyCost { get; set; }
        public int Reward { get; set; }
        public int RewardTypeId { get; set; }
        public string RewardName { get; set; }
        public string SkillCategoryName { get; set; }
        public int MinimalLevel { get; set; }

        public Hack()
        {

        }
        public Hack(string name, string description, int baseDifficulty, int skillDifficulty, int energyCost, int reward)
        {
            Name = name;
            Description = description;
            BaseDifficulty = baseDifficulty;
            SkillDifficulty = skillDifficulty;
            EnergyCost = energyCost;
            Reward = reward;
        }

    }
}