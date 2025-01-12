using System.Data;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using Npgsql.Replication;
using Npgsql.Replication.PgOutput.Messages;
using Server.Gameplay.Battle;
using Server.Repositories;
using Server.Utilities;
using Shared.DTOs;
using Shared.Networking.Http;

namespace Server.Controller;

public static class BattleController
{
    public static async Task<HttpResponseMessage> StartBattleAsync(HttpRequestMessage request)
    {
        if (!HttpUtilities.TryGetAuthenticationToken(request, out string? token) ||
            !TokenUtilities.ValidateToken(token!, out ClaimsPrincipal? principal))
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.Unauthorized,
                "Authorization token is missing or invalid.");
        }

        int userId = Int32.Parse(principal!.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        UserDto? user1 = await RepositoryManager.UserRepository.GetByIdAsync(userId);

        if (user1 == null)
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.NotFound, "User does not exist.");
        }

        IEnumerable<UserDto> playersInMmrRange =
            await RepositoryManager.UserRepository.FindUsersInMmrRangeAsync(user1.Mmr, BattleManager.MmrRange);

        Random random = new Random();

        UserDto user2 = playersInMmrRange.OrderBy(_ => random.Next()).First();

        IEnumerable<CardDto> deckUser1 = await RepositoryManager.UserCardsRepository.GetCardsInDeckAsync(user1.UserId);
        IEnumerable<CardDto> deckUser2 = await RepositoryManager.UserCardsRepository.GetCardsInDeckAsync(user2.UserId);


        BattleEntity battleEntityUser1 = new BattleEntity { UserId = user1.UserId };
        battleEntityUser1.LoadDeck(deckUser1);

        BattleEntity battleEntityUser2 = new BattleEntity { UserId = user2.UserId };
        battleEntityUser2.LoadDeck(deckUser2);

        BattleDto battleDto = await RepositoryManager.BattleRepository.AddAsync(new BattleDto
        {
            Player1Id = user1.UserId,
            Player2Id = user2.UserId,
        });

        BattleLog battleLog = BattleManager.Battle(battleEntityUser1, battleEntityUser2);

        battleDto.WinnerId = battleLog.WinnerUserId;

        await RepositoryManager.BattleRepository.UpdateAsync(battleDto);

        foreach (BattleRoundDto round in battleLog.Rounds)
        {
            round.BattleId = battleDto.BattleId;
            await RepositoryManager.BattleRepository.LogBattleRoundAsync(round);
        }

        Console.WriteLine(user1.Password);
        Console.WriteLine(user2.Password);
        
        if (battleLog.WinnerUserId != null)
        {
            user1.Mmr = battleLog.WinnerUserId == user1.UserId ? user1.Mmr + 30 : user1.Mmr - 15;
            user2.Mmr = battleLog.WinnerUserId == user2.UserId ? user2.Mmr + 30 : user2.Mmr - 15;
            await RepositoryManager.UserRepository.UpdateAsync(user1);
            await RepositoryManager.UserRepository.UpdateAsync(user2);
        }

        return HttpUtilities.GenerateResponse(HttpStatusCode.OK, JsonSerializer.Serialize(battleLog.Rounds));
    }
}