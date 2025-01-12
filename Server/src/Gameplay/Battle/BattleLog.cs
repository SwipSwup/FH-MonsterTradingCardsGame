using Shared.DTOs;

namespace Server.Gameplay.Battle;

public class BattleLog
{
    public int? WinnerUserId;
    public int? LoserUserId;

    public List<BattleRoundDto> Rounds = new();
}