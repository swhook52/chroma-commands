using System.Drawing;
using ChromaCommand.Api.Services;
using ChromaCommand.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChromaCommand.Api.Test
{
    [TestClass]
    public class StyleParserTest
    {
        private StyleParser _uut;

        [TestInitialize]
        public void Setup()
        {
            _uut = new StyleParser();
        }

        [TestMethod]
        public void Parse_MultileCommands_IdentifiesCommands()
        {
            // Arrange
            var rawCommand = "Pulse #FF0000 THEN clear THEN spell steve THEN wait 1000 THEN pulse #00FF00";
            
            // Act
            var result = _uut.Parse(rawCommand);

            // Assert
            Assert.AreEqual(5, result.Commands.Length);
        }

        [TestMethod]
        public void Parse_Pulse_MapsToStyle()
        {
            // Arrange
            var rawCommand = "Pulse #FF0000";

            // Act
            var result = _uut.Parse(rawCommand);

            // Assert
            var command = result.Commands[0];
            Assert.AreEqual(ChromaAction.Pulse, command.Action);
            Assert.AreEqual(Color.FromArgb(1, 255, 0, 0), command.Color);
        }

        [TestMethod]
        public void Parse_Clear_MapsToStyle()
        {
            // Arrange
            var rawCommand = "Clear";

            // Act
            var result = _uut.Parse(rawCommand);

            // Assert
            var command = result.Commands[0];
            Assert.AreEqual(ChromaAction.Clear, command.Action);
        }

        [TestMethod]
        public void Parse_Spell_MapsToStyle()
        {
            // Arrange
            var rawCommand = "spell unittest";

            // Act
            var result = _uut.Parse(rawCommand);

            // Assert
            var command = result.Commands[0];
            Assert.AreEqual(ChromaAction.Spell, command.Action);
            Assert.AreEqual("unittest", command.Parameter);
        }

        [TestMethod]
        public void Parse_Wait_MapsToStyle()
        {
            // Arrange
            var rawCommand = "Wait 1000";

            // Act
            var result = _uut.Parse(rawCommand);

            // Assert
            var command = result.Commands[0];
            Assert.AreEqual(ChromaAction.Wait, command.Action);
            Assert.AreEqual("1000", command.Parameter);
        }

        [TestMethod]
        public void Parse_Static_MapsToStyle()
        {
            // Arrange
            var rawCommand = "Static #FF0000";

            // Act
            var result = _uut.Parse(rawCommand);

            // Assert
            var command = result.Commands[0];
            Assert.AreEqual(ChromaAction.Static, command.Action);
            Assert.AreEqual(Color.FromArgb(1, 255, 0, 0), command.Color);
        }

        [TestMethod]
        public void Parse_Reactive_MapsToStyle()
        {
            // Arrange
            var rawCommand = "Reactive #FF0000";

            // Act
            var result = _uut.Parse(rawCommand);

            // Assert
            var command = result.Commands[0];
            Assert.AreEqual(ChromaAction.Reactive, command.Action);
            Assert.AreEqual(Color.FromArgb(1, 255, 0, 0), command.Color);
        }

        [TestMethod]
        public void Parse_Wave_MapsToStyle()
        {
            // Arrange
            var rawCommand = "Wave left";

            // Act
            var result = _uut.Parse(rawCommand);

            // Assert
            var command = result.Commands[0];
            Assert.AreEqual(ChromaAction.Wave, command.Action);
            Assert.AreEqual("LEFT", command.Parameter);
        }

        [TestMethod]
        public void Parse_Cycle_MapsToStyle()
        {
            // Arrange
            var rawCommand = "Cycle";

            // Act
            var result = _uut.Parse(rawCommand);

            // Assert
            var command = result.Commands[0];
            Assert.AreEqual(ChromaAction.Cycle, command.Action);
        }

    }
}
