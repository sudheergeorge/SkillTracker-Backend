using Admin.Application.Contracts;
using Admin.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SkillTracker.Entities;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Admin.Application.Features.Queries.SearchQuery
{
    public class SearchProfileQueryHandler : IRequestHandler<SearchProfileQuery, IEnumerable<Profile>>
    {
        private readonly IPersonalInfoProvider _personalInfoProvider;
        private readonly ISkillProvider _skillProvider;
        // private readonly ICacheProvider _cache;
        private readonly AutoMapper.IMapper _mapper;
        private readonly ILogger<SearchProfileQueryHandler> _logger;

        public SearchProfileQueryHandler(
           //  ICacheProvider cache,
            ISkillProvider skillProvider,
            IPersonalInfoProvider personalInfoProvider,
            AutoMapper.IMapper mapper,
            ILogger<SearchProfileQueryHandler> logger
        )
        {
            // _cache = cache;
            _skillProvider = skillProvider;
            _personalInfoProvider = personalInfoProvider;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Profile>> Handle(SearchProfileQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(request.EmpId))
            {
                var personalInfo = await _personalInfoProvider.SearchByEmpIdAsync(request.EmpId);
                return new List<Profile>
                {
                    await GetProfile(personalInfo)
                };
            }
            else if (!string.IsNullOrWhiteSpace(request.Name))
            {
                var personalInfoList = await _personalInfoProvider.SearchByNameAsync(request.Name);
                return await GetProfiles(personalInfoList);
            }
            else if (!string.IsNullOrWhiteSpace(request.Skill))
            {
                var keys = await _skillProvider.SearchForKeyAsync(request.Skill);
                return await GetProfiles(keys);
            }
            return null;
        }

        private async Task<IEnumerable<Profile>> GetProfiles(IList<PersonalInfoEntity> personalInfoList)
        {
            ConcurrentBag<Profile> profiles = new ConcurrentBag<Profile>();

            var tasks = personalInfoList.Select(async pinfo =>
            {
                var profile = await GetProfile(pinfo);
                profiles.Add(profile);
            });
            await Task.WhenAll(tasks);

            return profiles;
        }

        private async Task<Profile> GetProfile(PersonalInfoEntity personalInfo)
        {
            var skills = await _skillProvider.GetAsync(personalInfo.EmpId);
            var profile = _mapper.Map<Profile>(personalInfo);
            profile.Skills = skills.Select(s => _mapper.Map<Skill>(s)).ToList();
            return profile;
        }

        private async Task<IEnumerable<Profile>> GetProfiles(IList<string> keys)
        {
            ConcurrentBag<Profile> profiles = new ConcurrentBag<Profile>();

            var tasks = keys.Select(async key =>
            {
                var personalInfo = await _personalInfoProvider.SearchByEmpIdAsync(key);
                var profile = await GetProfile(personalInfo);
                profiles.Add(profile);
            });
            await Task.WhenAll(tasks);

            return profiles;
        }

    }
}
