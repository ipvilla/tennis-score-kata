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

        [Test]
        public void increase_player_1_score_to_15_when_player_1_scores_one_point()
        {
            var printer = Substitute.For<IScorePrinter>();
            var tennisGame = new TennisGame(printer);
            tennisGame.PlayerOneScores();

            tennisGame.PrintScore();

            printer.Received().Print("Current score: 15 - love");
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

        public void PlayerOneScores()
        {
        }
    }

    public interface IScorePrinter
    {
        void Print(string scoreMessage);
    }
}