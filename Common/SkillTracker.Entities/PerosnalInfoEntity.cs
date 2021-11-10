using Amazon.DynamoDBv2.DataModel;

namespace SkillTracker.Entities
{
    [DynamoDBTable("st-personal_info")]
    public class PersonalInfoEntity: EntityBase
    {
        [DynamoDBProperty("empId")]
        [DynamoDBHashKey]
        public string EmpId { get; set; }

        [DynamoDBProperty("userId")]
        [DynamoDBRangeKey]
        public string UserId { get; set; }

        [DynamoDBProperty("name")]
        public string Name { get; set; }

        [DynamoDBProperty("email")]
        public string Email { get; set; }

        [DynamoDBProperty("mobile")]
        public string Mobile { get; set; }

    }
}
