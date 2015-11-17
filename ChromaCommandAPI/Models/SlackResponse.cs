namespace ChromaCommand.Api.Models
{
    public class SlackResponse
    {
        /*{
            "response_type": "in_channel",
            "text": "It's 80 degrees right now.",
            "attachments": [
                {
                    "text":"Partly cloudy today and tomorrow"
                }
            ]
        }*/
        public string response_type { get; set; }
        public string text { get; set; }
        public SlackAttachment[] attachments { get; set; }
    }
}