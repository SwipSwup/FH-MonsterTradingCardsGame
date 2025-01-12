namespace Shared.DTOs
{
    public class BattleDto
    {
        public int BattleId { get; set; }
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public int? WinnerId { get; set; }
    }
}