namespace Model
{
    public class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillCategory SkillCategory { get; set; }

        public Skill()
        {

        }
        public Skill(string name, string description, SkillCategory skillCategory)
        {
            Name = name;
            Description = description;
            SkillCategory = skillCategory;
        }
    }
}