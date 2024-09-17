namespace MonsterTradingCardsGame.Gameplay.User;

public struct UserStats
{
    public int Wins { get; private set; }
    
    public int Losses { get; private set; }

    public UserStats(int wins, int losses)
    {
        Wins = wins;
        Losses = losses;
    }

    public void IncrementWins()
    {
        Wins++;
    }

    public void IncrementLosses()
    {
        Losses++;
    }
}