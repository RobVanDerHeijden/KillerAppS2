namespace Model
{
    public class SkillCategory
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public SkillCategory()
        {

        }
        public SkillCategory(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}