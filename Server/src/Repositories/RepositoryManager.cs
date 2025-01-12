namespace Server.Repositories;

public static class RepositoryManager
{
    public static UserRepository UserRepository { get; private set; } = new();
    
    public static CardRepository CardRepository { get; private set; } = new();
    
    public static UserCardsRepository UserCardsRepository { get; private set; } = new();
    
    public static BattleRepository BattleRepository { get; private set; } = new();
}