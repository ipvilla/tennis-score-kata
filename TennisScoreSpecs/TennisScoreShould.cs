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

        [Test]
        public void say_that_player_one_has_advantage_if_player_one_scores_when_both_players_are_deuce()
        {
            var printer = Substitute.For<IScorePrinter>();
            var tennisGame = new TennisGame(printer);
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerOneScores();

            tennisGame.PrintScore();

            printer.Received().Print("Current score: advantage - 40");
        }

        [Test]
        public void say_that_player_two_has_advantage_if_player_two_scores_when_both_players_are_deuce()
        {
            var printer = Substitute.For<IScorePrinter>();
            var tennisGame = new TennisGame(printer);
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();

            tennisGame.PrintScore();

            printer.Received().Print("Current score: 40 - advantage");
        }

        [Test]
        public void say_that_players_are_deuce_when_the_player_that_has_no_advantage_scores()
        {
            var printer = Substitute.For<IScorePrinter>();
            var tennisGame = new TennisGame(printer);
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerOneScores();

            tennisGame.PrintScore();

            printer.Received().Print("Current score: deuce");
        }

        [Test]
        public void say_that_player_one_won_when_he_scored_when_having_advantage()
        {
            var printer = Substitute.For<IScorePrinter>();
            var tennisGame = new TennisGame(printer);
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();

            tennisGame.PrintScore();

            printer.Received().Print("Current score: Player one wins!");
        }

        [Test]
        public void say_that_player_one_won_when_he_scored_four_points_and_two_more_than_player_two()
        {
            var printer = Substitute.For<IScorePrinter>();
            var tennisGame = new TennisGame(printer);
            tennisGame.PlayerOneScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();

            tennisGame.PrintScore();

            printer.Received().Print("Current score: Player one wins!");
        }

        [Test]
        public void say_that_player_one_won_when_he_scored_four_points_and_more_than_two_points_ahead_of_player_two()
        {
            var printer = Substitute.For<IScorePrinter>();
            var tennisGame = new TennisGame(printer);
            tennisGame.PlayerOneScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();

            tennisGame.PrintScore();

            printer.Received().Print("Current score: Player one wins!");
        }

        [Test]
        public void say_that_player_two_won_when_he_scored_when_having_advantage()
        {
            var printer = Substitute.For<IScorePrinter>();
            var tennisGame = new TennisGame(printer);
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();

            tennisGame.PrintScore();

            printer.Received().Print("Current score: Player two wins!");
        }

        [Test]
        public void say_that_player_two_won_when_he_scored_four_points_and_more_than_two_ahead_of_player_one()
        {
            var printer = Substitute.For<IScorePrinter>();
            var tennisGame = new TennisGame(printer);
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();

            tennisGame.PrintScore();

            printer.Received().Print("Current score: Player two wins!");
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
            printer.Print($"Current score: {GetCombinedScore()}");
        }

        private string GetIndividualScore(int score)
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
        private string GetCombinedScore()
        {
            if (PlayerOneWon())
            {
                return "Player one wins!";
            }
            if (playerOneScore >= 3 && playerTwoScore >= 3 && playerTwoScore == playerOneScore + 2)
            {
                return "Player two wins!";
            }
            if (PlayerOneHasAdvantage())
            {
                return $"advantage - {GetIndividualScore(playerTwoScore)}";
            }
            if (PlayerTwoHasAdvantage())
            {
                return $"{GetIndividualScore(playerOneScore)} - advantage";
            }

            return PlayersAreDeuce() ? "deuce" : $"{GetIndividualScore(playerOneScore)} - {GetIndividualScore(playerTwoScore)}";
        }

        private bool PlayerOneWon()
        {
            return playerOneScore > 3 && playerOneScore >= playerTwoScore + 2;
        }

        private bool PlayerOneHasAdvantage()
        {
            return playerOneScore > 3 && playerTwoScore >= 3 && playerOneScore == playerTwoScore + 1;
        }

        private bool PlayerTwoHasAdvantage()
        {
            return playerTwoScore > 3 && playerOneScore >= 3 && playerTwoScore == playerOneScore + 1;
        }

        private bool PlayersAreDeuce()
        {
            return playerOneScore == playerTwoScore && playerOneScore >= 3;
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