namespace ChromaCommand.Api.Models
{
    public class SlackAttachment
    {
        /*{
            "fallback": "Required plain-text summary of the attachment.",

            "color": "#36a64f",

            "pretext": "Optional text that appears above the attachment block",

            "author_name": "Bobby Tables",
            "author_link": "http://flickr.com/bobby/",
            "author_icon": "http://flickr.com/icons/bobby.jpg",

            "title": "Slack API Documentation",
            "title_link": "https://api.slack.com/",

            "text": "Optional text that appears within the attachment",

            "fields": [
                {
                    "title": "Priority",
                    "value": "High",
                    "short": false
                }
            ],

            "image_url": "http://my-website.com/path/to/image.jpg",
            "thumb_url": "http://example.com/path/to/thumb.png"
        }*/
        public string fallback { get; set; }
        public string color { get; set; }
        public string pretext { get; set; }
        public string author_name { get; set; }
        public string author_link { get; set; }
        public string author_icon { get; set; }
        public string title { get; set; }
        public string title_link { get; set; }
        public string text { get; set; }
        public SlackField[] fields { get; set; }
        public string image_url { get; set; }
        public string thumb_url { get; set; }
    }
}