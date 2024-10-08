namespace MonsterTradingCardsGame.Core.Networking.Server.Repositories;

public class RepositoryManager
{
    public static UserCredentialsRepository UserCredentialsRepository { get; private set; } = new();
}