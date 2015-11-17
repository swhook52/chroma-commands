using System.Drawing;
using System.Text;

namespace ChromaCommand.Dto
{
    public class StyleCommand
    {
        public ChromaAction Action { get; set; }
        public string Parameter { get; set; }
        public Color Color { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder(Action.ToString());

            if (Color != Color.Empty)
                builder.Append($" #{Color.R:x2}{Color.G:x2}{Color.B:x2}");

            if (!string.IsNullOrEmpty(Parameter))
            {
                builder.Append(" ");
                builder.Append(Parameter);
            }

            return builder.ToString();
        }
    }
}