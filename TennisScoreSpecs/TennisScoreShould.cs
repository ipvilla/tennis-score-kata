using NSubstitute;
using NUnit.Framework;

namespace TennisScoreSpecs
{
    public class TennisScoreShould
    {
        [Test]
        public void print_initial_score()
        {
            var printer = Substitute.For<IScorePrinter>();
            var tennisGame = new TennisGame(printer);

            tennisGame.PrintScore();

            printer.Received().Print("Current score: love - love");
        }
    }

    public class TennisGame
    {
        private readonly IScorePrinter printer;

        public TennisGame(IScorePrinter printer)
        {
            this.printer = printer;
        }

        public void PrintScore()
        {
            printer.Print("Current score: love - love");
        }
    }

    public interface IScorePrinter
    {
        void Print(string scoreMessage);
    }
}