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
        private readonly IScorePrinter printer;

        public TenisGame(IScorePrinter printer)
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