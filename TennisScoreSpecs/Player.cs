namespace TennisScoreSpecs
{
    public class Player
    {
        public int Score { get; private set; }
        public bool IsWinner { get; internal set; }

        public void WinAPoint()
        {
            Score++;
        }

        public string GetScore()
        {
            switch (Score)
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
            return Score > 3;
        }

        public bool HasAtLeastThreePoints()
        {
            return Score >= 3;
        }

        public bool HasAtLeastTwoPointsMoreThan(int referencePoints)
        {
            return Score >= referencePoints + 2;
        }

        public bool HasOnePointMoreThan(int referencePoints)
        {
            return Score == referencePoints + 1;
        }
    }
}