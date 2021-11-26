using Admin.Application.Features.Commands.CacheProfile;
using AutoMapper;
using EventBus.Messaging.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Admin.API.EventBusConsumers
{
    public class AddProfileConsumer : IConsumer<AddProfileEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<AddProfileConsumer> _logger;

        public AddProfileConsumer(IMediator mediator, IMapper mapper, ILogger<AddProfileConsumer> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task Consume(ConsumeContext<AddProfileEvent> context)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            var command = JsonConvert.DeserializeObject<CacheProfileCommand>(context.Message.Data);
            _ = _mediator.Send(command);

            //var escommand = JsonConvert.DeserializeObject<ESCacheProfileCommand>(context.Message.Data);
            //_ = _mediator.Send(escommand);
        }
    }
}
