namespace MonsterTradingCardsGame.Gameplay.User;

public struct UserStats
{
    public int Mmr { get; private set; }
    
    public int Wins { get; private set; }
    
    public int Losses { get; private set; }

    public UserStats(int wins, int losses, int mmr)
    {
        Wins = wins;
        Losses = losses;
        Mmr = mmr;
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