namespace MonsterTradingCardsGame.Gameplay.User;

public struct UserCredentials
{
    public int Mmr { get; private set; }
    
    public string Username { get; private set; }
    
    //Token

    public UserCredentials(int mmr, string username)
    {
        Mmr = mmr;
        Username = username;
    }
}
