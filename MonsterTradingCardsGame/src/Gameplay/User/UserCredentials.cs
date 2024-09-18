namespace MonsterTradingCardsGame.Gameplay.User;

public struct UserCredentials
{
    public string Username { get; private set; }
    
    //Token

    public UserCredentials(string username)
    {
        Username = username;
    }
}
