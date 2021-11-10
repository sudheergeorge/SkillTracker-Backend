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

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> Handle(CacheProfileCommand request, CancellationToken cancellationToken)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            var profile = _mapper.Map<Profile>(request);

            _cacheRepo.Set<object>(profile.EmpId, profile);

            _logger.LogInformation($"Profile {profile.EmpId} is successfully cached.");

            return profile.EmpId;
        }
    }
}
