namespace Profile.Domain.Entities
{
    public class Skill
    {
        public int SkillId { get; set; }

        public bool IsTechnical { get; set; }

        public string Name { get; set; }

        public int Proficiency { get; set; }
    }
}
