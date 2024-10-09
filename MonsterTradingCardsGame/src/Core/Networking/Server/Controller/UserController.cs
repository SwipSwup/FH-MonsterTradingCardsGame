using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using MonsterTradingCardsGame.Core.Networking.DTOs.Authentication;
using MonsterTradingCardsGame.Core.Networking.Http;
using MonsterTradingCardsGame.Core.Networking.Server.Repositories;
using MonsterTradingCardsGame.Core.Networking.Server.Utilities;

namespace MonsterTradingCardsGame.Core.Networking.Server.Controller;

public static class UserController
{
    public static async Task<string> RegisterUserAsync(HttpRequest request)
    {
        //string json = await new StreamReader(request.Body).ReadToEndAsync();
        if (!request.Headers.TryGetValue(HttpHeader.ContentType, out string contentType) ||
            contentType != "application/json")
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.UnsupportedMediaType, "Unsupported Media Type",
                "Content-Type must be application/json");
        }

        UserCredentialsDto userCredentialsDto =
            await JsonSerializer.DeserializeAsync<UserCredentialsDto>(request.Body);
        
        if (string.IsNullOrWhiteSpace(userCredentialsDto.Username) ||
            string.IsNullOrWhiteSpace(userCredentialsDto.Password))
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.BadRequest, "Bad Request",
                "Username and password are required.");
        }

        //Check if Username already excists
        UserCredentialsDto? databaseUserCredentials =
            await RepositoryManager.UserCredentialsRepository.GetByIdAsync(userCredentialsDto.Username);
        if (databaseUserCredentials != null)
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.Conflict, "Conflict", "Username already exists.");
        }

        //TODO Create new User

        userCredentialsDto.Password = PasswordUtilities.HashPassword(userCredentialsDto.Password);
        await RepositoryManager.UserCredentialsRepository.AddAsync(userCredentialsDto);

        AuthenticationResponseDto responseDto = new AuthenticationResponseDto
        {
            Success = true,
            Message = "Registration Success",
            Token = TokenUtilities.GenerateJwtToken(userCredentialsDto.Username)
        };

        return HttpUtilities.GenerateResponse(HttpStatusCode.Created, JsonSerializer.Serialize(responseDto));
    }

    public static async Task<string> LoginUserAsync(HttpRequest request)
    {
        if (!request.Headers.TryGetValue(HttpHeader.ContentType, out string contentType) ||
            contentType != "application/json")
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.UnsupportedMediaType, "Unsupported Media Type",
                "Content-Type must be application/json");
        }

        UserCredentialsDto userCredentialsDto =
            await JsonSerializer.DeserializeAsync<UserCredentialsDto>(new StreamReader(request.Body).BaseStream);

        if (string.IsNullOrWhiteSpace(userCredentialsDto.Username) ||
            string.IsNullOrWhiteSpace(userCredentialsDto.Password))
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.BadRequest, "Bad Request",
                "Username and password are required.");
        }

        //DB call
        UserCredentialsDto? databaseUserCredentials =
            await RepositoryManager.UserCredentialsRepository.GetByIdAsync(userCredentialsDto.Username);

        if (databaseUserCredentials == null)
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.NotFound, "User Not Found", "User does not exist.");
        }

        if (!PasswordUtilities.VerifyPassword(userCredentialsDto.Password, databaseUserCredentials.Value.Password))
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.Unauthorized, "Wrong Password",
                "Password does not match.");
        }

        AuthenticationResponseDto responseDto = new AuthenticationResponseDto
        {
            Success = true,
            Message = "Login Success",
            Token = TokenUtilities.GenerateJwtToken(userCredentialsDto.Username)
        };

        return HttpUtilities.GenerateResponse(HttpStatusCode.OK, JsonSerializer.Serialize(responseDto));
    }
}