using System.Data;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Server.Gameplay.Cards;
using Server.Repositories;
using Server.Utilities;
using Shared.Card;
using Shared.Card.Types;
using Shared.DTOs;
using Shared.Networking.Http;

namespace Server.Controller;

public static class CardController
{
    public static async Task<HttpResponseMessage> OpenCardPackAsync(HttpRequestMessage request)
    {
        if (!HttpUtilities.TryGetAuthenticationToken(request, out string? token) ||
            !TokenUtilities.ValidateToken(token!, out ClaimsPrincipal? principal))
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.Unauthorized,
                "Authorization token is missing or invalid.");
        }

        int userId = Int32.Parse(principal!.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        if (Equals(request.Content?.Headers.ContentType, MediaTypeHeaderValue.Parse("application/json")))
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.UnsupportedMediaType,
                "Content-Type must be application/json");
        }

        if (request.Content == null)
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.BadRequest,
                "Expected a JSON body but none was provided.");
        }

        UserDto? userDto =
            await RepositoryManager.UserRepository.GetByIdAsync(userId);

        if (userDto == null)
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.NotFound,
                "The user with the provided ID or username does not exist.");
        }

        if (userDto.Currency < CardFactory.PackPrice)
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.BadRequest,
                "You do not have enough currency to buy this pack.");
        }

        Card[] pack = CardFactory.GetCardPack(CardFactory.PackSize);


        List<CardDto> responseDtos = new List<CardDto>();

        foreach (Card card in pack)
        {
            CardDto newUserCard = new CardDto
            {
                CardName = card.Name,
                Damage = card.Damage,
                Species = card is MonsterCard ? ((MonsterCard)card).Species : null,
                Rarity = card.Rarity,
                Element = card.Element,
                CardType = card is MonsterCard ? CardType.Monster : CardType.Spell
            };

            newUserCard.CardId = await RepositoryManager.CardRepository.GetCardIdIfExistsAsync(
                newUserCard.CardName,
                newUserCard.Damage,
                (int?)newUserCard.Species,
                (int)newUserCard.Rarity,
                (int)newUserCard.Element,
                (int)newUserCard.CardType
            ) ?? (await RepositoryManager.CardRepository.AddAsync(newUserCard)).CardId;

            await RepositoryManager.UserCardsRepository.AddAsync(new UserCardsDto
            {
                UserId = userId,
                CardId = newUserCard.CardId,
                IsInDeck = false,
                IsInTrade = false
            });

            responseDtos.Add(newUserCard);
        }

        userDto.Currency -= CardFactory.PackPrice;
        await RepositoryManager.UserRepository.UpdateAsync(userDto);

        return HttpUtilities.GenerateResponse(HttpStatusCode.Created, JsonSerializer.Serialize(responseDtos));
    }

    public static async Task<HttpResponseMessage> UpdateUserDeckAsync(HttpRequestMessage request)
    {
        if (!HttpUtilities.TryGetAuthenticationToken(request, out string? token) ||
            !TokenUtilities.ValidateToken(token!, out ClaimsPrincipal? principal))
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.Unauthorized,
                "Authorization token is missing or invalid.");
        }

        int userId = Int32.Parse(principal!.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        if (Equals(request.Content?.Headers.ContentType, MediaTypeHeaderValue.Parse("application/json")))
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.UnsupportedMediaType,
                "Content-Type must be application/json");
        }

        if (request.Content == null)
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.BadRequest,
                "Expected a JSON body but none was provided.");
        }

        DeckDto deckDto = (await JsonSerializer.DeserializeAsync<DeckDto>(await request.Content!.ReadAsStreamAsync()))!;

        if (deckDto.CardIds.Length > 4)
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.BadRequest,
                "Too many card ids provided.");
        }

        IEnumerable<UserCardsDto> userCardsDtos = await RepositoryManager.UserCardsRepository.GetAllByUserIdAsync(userId);
        
        foreach (UserCardsDto dto in userCardsDtos)
        {
            dto.IsInDeck = false;

            await RepositoryManager.UserCardsRepository.UpdateAsync(dto);
        }

        foreach (int cardId in deckDto.CardIds)
        {
            UserCardsDto? userCardsDto = await RepositoryManager.UserCardsRepository.GetByIdAsync(userId, cardId);

            if (userCardsDto == null)
            {
                return HttpUtilities.GenerateErrorResponse(HttpStatusCode.BadRequest,
                    $"The player does not have card with id {cardId}.");
            }
            
            userCardsDto.IsInDeck = true;
            
            await RepositoryManager.UserCardsRepository.UpdateAsync(userCardsDto);
        }
        
        return HttpUtilities.GenerateResponse(HttpStatusCode.OK, "Player deck updated.");
    }
}