using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Admin.Application.Contracts;
using Admin.Domain.Models;

namespace Admin.Application.Features.Commands.CacheProfile
{
    public class CacheProfileCommandHandler : IRequestHandler<CacheProfileCommand, string>
    {
        private readonly ICacheRepository _cacheRepo;
        private readonly AutoMapper.IMapper _mapper;
        private readonly ILogger<CacheProfileCommandHandler> _logger;


        public CacheProfileCommandHandler(ICacheRepository cacheRepo, AutoMapper.IMapper mapper, ILogger<CacheProfileCommandHandler> logger)
        {
            _cacheRepo = cacheRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(CacheProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = _mapper.Map<Profile>(request);

            await _cacheRepo.SetAsync<object>(profile.EmpId, profile);

            _logger.LogInformation($"Profile {profile.EmpId} is successfully cached.");

            return profile.EmpId;
        }
    }
}
