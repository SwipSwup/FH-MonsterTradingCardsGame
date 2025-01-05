using Client.Engine.ConsoleManager;

namespace Client;

public class MtcgClient
{
    private ConsoleManager _consoleManager;
    
    public MtcgClient()
    {
        _consoleManager = new ConsoleManager("Mtcg", 100, 100);
    }
    
    
    
    
}