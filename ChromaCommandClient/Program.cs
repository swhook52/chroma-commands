using System;
using System.Drawing;
using System.Threading;
using ChromaCommand.Dto;
using Corale.Colore.Razer.Keyboard.Effects;
using Microsoft.ServiceBus.Messaging;

namespace ChromaCommand.Client
{
    class Program
    {
        private static Corale.Colore.Core.IKeyboard _keyboard;

        static void Main(string[] args)
        {
            _keyboard = Corale.Colore.Core.Keyboard.Instance;
            const string connectionString = "Endpoint=sb://chromacommandqueue-ns.servicebus.windows.net/;SharedAccessKeyName=Receiver;SharedAccessKey=cr8iOVFr2dXI9xrdgKfXp3+OsMFoeW9EigSCRoHmFyc=";
            var queue = QueueClient.CreateFromConnectionString(connectionString, "ChromaCommandQueue", ReceiveMode.ReceiveAndDelete);

            while (true)
            {
                var message = queue.Receive();
                if (message == null)
                    continue;

                _keyboard.Clear();
                var style = message.GetBody<ChromaStyle>();
                Console.WriteLine("---------------------");
                foreach (var command in style.Commands)
                {
                    Console.WriteLine($"{command.Action} rgb({command.Color.R},{command.Color.G},{command.Color.B}) {command.Parameter}");
                    StyleKeyboard(command);
                }
                Console.WriteLine("---------------------");
                Console.WriteLine();
            }
        }

        private static void StyleKeyboard(StyleCommand command)
        {
            switch (command.Action)
            {
                case ChromaAction.Pulse:
                    StylePulse(command.Color);
                    break;
                case ChromaAction.Clear:
                    _keyboard.Clear();
                    break;
                case ChromaAction.Spell:
                    StyleSpell(command.Parameter, command.Color);
                    break;
                case ChromaAction.Wait:
                    Wait(command.Parameter);
                    break;
                case ChromaAction.Cycle:
                    StyleCycle();
                    break;
                case ChromaAction.Reactive:
                    StyleReactive(command.Color);
                    break;
                case ChromaAction.Static:
                    StyleStatic(command.Color);
                    break;
                case ChromaAction.Wave:
                    StyleWave(command.Parameter);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void StyleStatic(Color color)
        {
            var staticColor = new Corale.Colore.Core.Color(color.R, color.G, color.B, color.A);
            _keyboard.SetStatic(new Static(staticColor));
        }

        private static void StyleReactive(Color color)
        {
            _keyboard.SetReactive(
                new Corale.Colore.Core.Color(color.R, color.G, color.B, color.A),
                Duration.Medium);
        }

        private static void StyleCycle()
        {
            _keyboard.SetEffect(Effect.SpectrumCycling);
        }

        private static void StyleWave(string direction)
        {
            if (direction.ToLower() == "left")
            {
                _keyboard.SetWave(Direction.RightToLeft);
            }
            else
            {
                _keyboard.SetWave(Direction.LeftToRight);
            }
        }

        

        private static void StyleSpell(string word, Color color)
        {
            //foreach (char letter in word.ToLower())
            //{
            //    Corale.Colore.Razer.Keyboard.Key key = new Corale.Colore.Razer.Keyboard.Key();
            //    if (key.TryParse(letter, out key))
            //    {
            //        _keyboard.SetKey(
            //            key,
            //            Corale.Colore.Core.Color(color.R, color.G, color.B, color.A));
            //        Thread.Sleep(1000);
            //    }
            //}
        }

        private static void Wait(string milliseconds)
        {
            int waitTime;
            if (int.TryParse(milliseconds, out waitTime))
            {
                Thread.Sleep(waitTime);
            }
        }

        private static void StylePulse(Color color)
        {
            var pulseColor = new Corale.Colore.Core.Color(color.R, color.G, color.B, color.A);
            _keyboard.SetBreathing(pulseColor, pulseColor);
        }
    }
}
