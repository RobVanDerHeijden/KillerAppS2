using System;

namespace Model
{
    public class Relation
    {
        public Player PlayerInRelationWith { get; set; }
        public int RelationStatus { get; set; }
        public DateTime Since { get; set; }

        public Relation()
        {

        }
        public Relation(Player playerInRelationWith, int relationStatus, DateTime since)
        {
            PlayerInRelationWith = playerInRelationWith;
            RelationStatus = relationStatus;
            Since = since;
        }
    }
}