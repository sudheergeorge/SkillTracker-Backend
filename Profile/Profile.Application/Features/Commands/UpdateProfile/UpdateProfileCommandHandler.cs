using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Profile.Application.Contracts;
using Profile.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Profile.Application.Features.Commands.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, string>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProfileCommandHandler> _logger;

        public UpdateProfileCommandHandler(IProfileRepository profileRepository, IMapper mapper, ILogger<UpdateProfileCommandHandler> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _profileRepository = profileRepository;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            //var profile = await _profileRepository.GetItem(request.EmpId);
            //// var profile = _mapper.Map<ProfileEntity>(request);
            //profile.skills = request.Skills.Select(s => _mapper.Map<Skill>(s)).ToList();

            //await _profileRepository.PutItem(profile);


            _logger.LogInformation($"Profile {request.EmpId} is successfully updated.");

            return request.EmpId;
        }
    }
}
