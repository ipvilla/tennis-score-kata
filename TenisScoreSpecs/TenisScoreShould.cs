using NSubstitute;
using NUnit.Framework;

namespace TenisScoreSpecs
{
    public class TenisScoreShould
    {
        [Test]
        public void print_initial_score()
        {
            var printer = Substitute.For<IScorePrinter>();
            var tenisGame = new TenisGame(printer);

            tenisGame.PrintScore();

            printer.Received().Print("Current score: love - love");
        }
    }

    public class TenisGame
    {
        public TenisGame(IScorePrinter printer)
        {
        }

        public void PrintScore()
        {
        }
    }

    public interface IScorePrinter
    {
        void Print(string scoreMessage);
    }
}