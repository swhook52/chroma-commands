using System.Configuration;
using System.Linq;
using System.Web.Http;
using ChromaCommand.Api.Models;
using ChromaCommand.Api.Services;
using ChromaCommand.Dto;
using Microsoft.ServiceBus.Messaging;

namespace ChromaCommand.Api.Controllers
{
    public class StyleController : ApiController
    {
        private readonly StyleParser _styleParser;

        public StyleController()
        {
            _styleParser = new StyleParser();
        }

        public SlackResponse Post(SlackMessage message)
        {
            var style = _styleParser.Parse(message.text);

            var connectionString = ConfigurationManager.AppSettings["ChromaStyleQueueWriter"];
            var queue = QueueClient.CreateFromConnectionString(connectionString, "ChromaCommandQueue");
            queue.Send(new BrokeredMessage(style));

            return GetSlackResponse(style);
        }

        private SlackResponse GetSlackResponse(ChromaStyle style)
        {
            return new SlackResponse
            {
                response_type = "in_channel",
                text = "Oh, lovely! I'm sure it will be appreciated. Here's what you sent:",
                attachments = style.Commands.Select(command => new SlackAttachment
                {
                    text = command.ToString(),
                    color = $"#{command.Color.R:x2}{command.Color.G:x2}{command.Color.B:x2}"
                }).ToArray()
            };
        }
    }

}
