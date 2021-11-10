using Amazon.DynamoDBv2.DataModel;
using Profile.Application.Contracts;
using Profile.Domain.Entities;
using SkillTracker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile.Infrastructure.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DynamoDBContext dbcontext;

        public SkillRepository(DynamoDBContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }


        public Task<SkillEntity> AddAsync(SkillEntity entity)
        {
            throw new NotImplementedException();
        }


        public async Task<IList<SkillEntity>> AddRangeAsync(IList<SkillEntity> entities)
        {
            var skillBatch = dbcontext.CreateBatchWrite<SkillEntity>();
            skillBatch.AddPutItems(entities);
            await skillBatch.ExecuteAsync();
            return entities;
        }

        public async Task DeleteAsync(object hashKey, object rangeKey)
        {
            await dbcontext.DeleteAsync<SkillEntity>(hashKey, rangeKey);
        }

        public Task<SkillEntity> GetItem(string hadhKey)
        {
            throw new NotImplementedException();
        }

        public Task PutItem(SkillEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(SkillEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
