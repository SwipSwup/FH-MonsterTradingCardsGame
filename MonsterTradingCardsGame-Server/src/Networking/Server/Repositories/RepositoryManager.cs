namespace MonsterTradingCardsGame.Networking.Server.Repositories;

public static class RepositoryManager
{
    public static UserCredentialsRepository UserCredentialsRepository { get; private set; } = new();
}