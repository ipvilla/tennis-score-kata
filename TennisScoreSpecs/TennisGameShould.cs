using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace TennisScoreSpecs
{
    public class TennisGameShould
    {
        private TennisGame tennisGame;
        private IScorePrinter printer;

        [SetUp]
        public void SetUp()
        {
            printer = Substitute.For<IScorePrinter>();
            tennisGame = new TennisGame(printer);
        }

        [Test]
        public void print_initial_score()
        {
            tennisGame.PrintScore();

            printer.Received().Print("Current score: love - love");
        }

        [Test]
        public void increase_player_1_score_to_15_when_player_1_scores_one_point()
        {
            tennisGame.PlayerOneScores();

            tennisGame.PrintScore();

            printer.Received().Print("Current score: 15 - love");
        }

        [Test]
        public void increase_player_2_score_to_15_when_player_2_scores_one_point()
        {
            tennisGame.PlayerTwoScores();

            tennisGame.PrintScore();

            printer.Received().Print("Current score: love - 15");
        }

        [Test]
        public void increase_player_score_to_30_when_player_scores_two_points()
        {
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();

            tennisGame.PrintScore();

            printer.Received().Print("Current score: 30 - love");
        }

        [Test]
        public void increase_player_score_to_40_when_player_scores_three_points()
        {
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();

            tennisGame.PrintScore();

            printer.Received().Print("Current score: 40 - love");
        }

        [Test]
        public void say_that_players_are_deuce_if_they_both_score_three_points()
        {
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
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();

            tennisGame.PrintScore();

            printer.Received().Print("Current score: Player two wins!");
        }

        [Test]
        public void not_increase_player_one_score_after_player_one_has_won()
        {
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            var currentScore = tennisGame.GetPlayerOneScore();

            tennisGame.PlayerOneScores();
            
            tennisGame.GetPlayerOneScore().Should().Be(currentScore);
        }

        [Test]
        public void not_increase_player_one_score_after_player_two_has_won()
        {
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();
            tennisGame.PlayerTwoScores();
            var currentScore = tennisGame.GetPlayerOneScore();

            tennisGame.PlayerOneScores();
            
            tennisGame.GetPlayerOneScore().Should().Be(currentScore);
        }

        [Test]
        public void not_increase_player_two_score_after_player_one_has_won()
        {
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            tennisGame.PlayerOneScores();
            var currentScore = tennisGame.GetPlayerTwoScore();

            tennisGame.PlayerTwoScores();

            tennisGame.GetPlayerTwoScore().Should().Be(currentScore);
        }

        [Test]
        public void not_increase_player_two_score_after_player_two_has_won()
        {
            tennisGame.GetPlayerTwoScore();
            tennisGame.GetPlayerTwoScore();
            tennisGame.GetPlayerTwoScore();
            tennisGame.GetPlayerTwoScore();
            var currentScore = tennisGame.GetPlayerTwoScore();

            tennisGame.PlayerTwoScores();

            tennisGame.GetPlayerTwoScore().Should().Be(currentScore);
        }
    }
}