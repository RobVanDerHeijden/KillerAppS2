using System;

namespace Model
{
    public class Achievement
    {
        public int AchievementId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateAchieved { get; set; }

        public Achievement()
        {

        }
        public Achievement(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}