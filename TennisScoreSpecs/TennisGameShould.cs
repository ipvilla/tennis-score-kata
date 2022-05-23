using System;
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
    }

    public class Player
    {
        public int score { get; private set; }

        public void WinAPoint()
        {
            score++;
        }

        public string GetScore()
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

        public bool HasMoreThanThreePoints()
        {
            return score > 3;
        }

        public bool HasAtLeastThreePoints()
        {
            return score >= 3;
        }

        public bool HasAtLeastTwoPointsMoreThan(int referencePoints)
        {
            return score >= referencePoints + 2;
        }

        public bool HasOnePointMoreThan(int referencePoints)
        {
            return score == referencePoints + 1;
        }
    }

    public class TennisGame
    {
        private readonly IScorePrinter printer;
        public readonly Player playerOne;
        private readonly Player playerTwo;

        public TennisGame(IScorePrinter printer)
        {
            this.printer = printer;
            playerOne = new Player();
            playerTwo = new Player();
        }

        public void PrintScore()
        {
            printer.Print($"Current score: {GetCombinedScore()}");
        }

        private string GetCombinedScore()
        {
            if (PlayerOneWon())
            {
                return "Player one wins!";
            }
            if (PlayerTwoWon())
            {
                return "Player two wins!";
            }
            if (PlayerOneHasAdvantage())
            {
                return $"advantage - {playerTwo.GetScore()}";
            }
            if (PlayerTwoHasAdvantage())
            {
                return $"{playerOne.GetScore()} - advantage";
            }

            return PlayersAreDeuce() ? "deuce" : $"{playerOne.GetScore()} - {playerTwo.GetScore()}";
        }

        private bool PlayerTwoWon()
        {
            return playerTwo.HasMoreThanThreePoints() && playerTwo.HasAtLeastTwoPointsMoreThan(playerOne.score);
        }

        private bool PlayerOneWon()
        {
            return playerOne.HasMoreThanThreePoints() && playerOne.HasAtLeastTwoPointsMoreThan(playerTwo.score);
        }

        private bool PlayerOneHasAdvantage()
        {
            return playerOne.HasMoreThanThreePoints() && playerTwo.HasAtLeastThreePoints() && playerOne.HasOnePointMoreThan(playerTwo.score);
        }

        private bool PlayerTwoHasAdvantage()
        {
            return playerTwo.HasMoreThanThreePoints() && playerOne.HasAtLeastThreePoints() && playerTwo.HasOnePointMoreThan(playerOne.score);
        }
        
        private bool PlayersAreDeuce()
        {
            return playerOne.score == playerTwo.score && playerOne.HasAtLeastThreePoints();
        }

        public void PlayerOneScores()
        {
            playerOne.WinAPoint();
        }

        public void PlayerTwoScores()
        {
            playerTwo.WinAPoint();
        }
        public int GetPlayerOneScore()
        {
            throw new NotImplementedException();
        }
    }

    public interface IScorePrinter
    {
        void Print(string scoreMessage);
    }
}