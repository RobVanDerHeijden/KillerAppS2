namespace Model
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SkillPoints { get; set; }
        public int MaxSkillPoints { get; set; }
        public string SkillCategoryName { get; set; }

        public Skill()
        {

        }
    }
}