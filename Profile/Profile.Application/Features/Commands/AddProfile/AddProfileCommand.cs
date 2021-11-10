using MediatR;
using Profile.Domain.Entities;
using System.Collections.Generic;

namespace Profile.Application.Features.Commands.AddProfile
{
    public class AddProfileCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string EmpId { get; set; }
        public string Mobile { get; set; }

        public List<Skill> Skills { get; set; }
    }
}
