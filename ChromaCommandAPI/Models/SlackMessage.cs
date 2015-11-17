namespace ChromaCommand.Api.Models
{
    public class SlackMessage
    {

        public string token { get; set; } // gIkuvaNzQIHg97ATvDxqgjtO
        public string team_id { get; set; } // T0001
        public string team_domain { get; set; } // example
        public string channel_id { get; set; } // C2147483705
        public string channel_name { get; set; } // test
        public string user_id { get; set; } // U2147483697
        public string user_name { get; set; } // Steve
        public string command { get; set; } // /weather
        public string text { get; set; } // 94070
        public string response_url { get; set; } // https://hooks.slack.com/commands/1234/5678

    }
}