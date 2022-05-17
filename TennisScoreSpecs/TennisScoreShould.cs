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

        [Test]
        public void increase_player_2_score_to_15_when_player_2_scores_one_point()
        {
            var printer = Substitute.For<IScorePrinter>();
            var tennisGame = new TennisGame(printer);
            tennisGame.PlayerTwoScores();

            tennisGame.PrintScore();

            printer.Received().Print("Current score: love - 15");
        }

        [Test]
        public void increase_player_score_to_30_when_player_scores_two_points()
        {
            var printer = Substitute.For<IScorePrinter>();
            var tennisGame = new TennisGame(printer);
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();

            tennisGame.PrintScore();

            printer.Received().Print("Current score: 30 - love");
        }

        [Test]
        public void increase_player_score_to_40_when_player_scores_three_points()
        {
            var printer = Substitute.For<IScorePrinter>();
            var tennisGame = new TennisGame(printer);
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();

            tennisGame.PrintScore();

            printer.Received().Print("Current score: 40 - love");
        }

        [Test]
        public void say_that_players_are_deuce_if_they_both_score_three_points()
        {
            var printer = Substitute.For<IScorePrinter>();
            var tennisGame = new TennisGame(printer);
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();

            tennisGame.PrintScore();

            printer.Received().Print("Current score: deuce");
        }
    }

    public class TennisGame
    {
        private readonly IScorePrinter printer;
        private int playerOneScore;
        private int playerTwoScore;

        public TennisGame(IScorePrinter printer)
        {
            this.printer = printer;
        }

        public void PrintScore()
        {
            if (playerOneScore == playerTwoScore && playerOneScore >= 3)
            {
                printer.Print("Current score: deuce");
            }
            else
            {
                string convertedPlayerOneScore = GetScore(playerOneScore);
                string convertedPlayerTwoScore = GetScore(playerTwoScore);

                printer.Print($"Current score: {convertedPlayerOneScore} - {convertedPlayerTwoScore}");
            }
        }

        private string GetScore(int score)
        {
            switch (score)
            {
                case 0:
                    return "love";
                case 1:
                    return "15";
                case 2:
                    return "30";
                default:
                    return "40";
            }
        }

        public void PlayerOneScores()
        {
            playerOneScore++;
        }

        public void PlayerTwoScores()
        {
            playerTwoScore++;
        }
    }

    public interface IScorePrinter
    {
        void Print(string scoreMessage);
    }
}