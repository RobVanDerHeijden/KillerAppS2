using System;

namespace Model
{
    public class Achievement
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateAchieved { get; set; }

        public Achievement(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}