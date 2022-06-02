namespace TennisScoreSpecs
{
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

        public void PlayerOneScores()
        {
            if (playerOne.IsWinner || playerTwo.IsWinner) return;

            playerOne.WinAPoint();
            if (PlayerOneWon())
            {
                playerOne.IsWinner = true;
            }

        }

        public void PlayerTwoScores()
        {
            playerTwo.WinAPoint();
            if (PlayerTwoWon())
            {
                playerTwo.IsWinner = true;
            }
        }

        public int GetPlayerOneScore()
        {
            return playerOne.Score;
        }

        private string GetCombinedScore()
        {
            if (PlayerOneWon()) return "Player one wins!";
            if (PlayerTwoWon()) return "Player two wins!";
            if (PlayerOneHasAdvantage()) return $"advantage - {playerTwo.GetScore()}";
            if (PlayerTwoHasAdvantage()) return $"{playerOne.GetScore()} - advantage";
            if (PlayersAreDeuce()) return "deuce";

            return $"{playerOne.GetScore()} - {playerTwo.GetScore()}";
        }

        private bool PlayerTwoWon()
        {
            return playerTwo.HasMoreThanThreePoints() && playerTwo.HasAtLeastTwoPointsMoreThan(playerOne.Score);
        }

        private bool PlayerOneWon()
        {
            return playerOne.HasMoreThanThreePoints() && playerOne.HasAtLeastTwoPointsMoreThan(playerTwo.Score);
        }

        private bool PlayerOneHasAdvantage()
        {
            return playerOne.HasMoreThanThreePoints() && playerTwo.HasAtLeastThreePoints() && playerOne.HasOnePointMoreThan(playerTwo.Score);
        }

        private bool PlayerTwoHasAdvantage()
        {
            return playerTwo.HasMoreThanThreePoints() && playerOne.HasAtLeastThreePoints() && playerTwo.HasOnePointMoreThan(playerOne.Score);
        }
        
        private bool PlayersAreDeuce()
        {
            return playerOne.Score == playerTwo.Score && playerOne.HasAtLeastThreePoints();
        }
    }
}