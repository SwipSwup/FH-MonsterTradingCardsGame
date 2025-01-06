using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Server.Repositories;
using Server.Utilities;
using Shared.DTOs.Authentication;
using Shared.Networking.DTOs.Authentication;
using Shared.Networking.Http;

namespace Server.Controller;

public static class UserController
{
    /*public static async Task<HttpResponseMessage> CreateUserAsync(HttpRequestMessage request)
    {
        if(RepositoryManager) {
                
        }
    }*/
    
    public static async Task<HttpResponseMessage> RegisterUserAsync(HttpRequestMessage request)
    {
        //string json = await new StreamReader(request.Body).ReadToEndAsync();
        //TODO was fehlt hier
        ;

        if (Equals(request.Content?.Headers.ContentType, MediaTypeHeaderValue.Parse("application/json")))
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.UnsupportedMediaType,
                "Content-Type must be application/json");
        }
        
        if (request.Content == null)
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.BadRequest, "Expected a JSON body but none was provided.");
        }

        UserCredentialsDto userCredentialsDto =
            (await JsonSerializer.DeserializeAsync<UserCredentialsDto>(await request.Content!.ReadAsStreamAsync()))!;

        if (string.IsNullOrWhiteSpace(userCredentialsDto.Username) ||
            string.IsNullOrWhiteSpace(userCredentialsDto.Password))
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.BadRequest, "Username and password are required.");
        }

        //Check if Username already excists
        UserCredentialsDto? databaseUserCredentials =
            await RepositoryManager.UserCredentialsRepository.GetByIdAsync(userCredentialsDto.Username);
        if (databaseUserCredentials != null)
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.Conflict, "Username already exists.");
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

    public static async Task<HttpResponseMessage> LoginUserAsync(HttpRequestMessage request)
    {
        if (Equals(request.Content?.Headers.ContentType, MediaTypeHeaderValue.Parse("application/json")))
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.UnsupportedMediaType,"Content-Type must be application/json");
        }

        UserCredentialsDto userCredentialsDto =
            (await JsonSerializer.DeserializeAsync<UserCredentialsDto>(await request.Content!.ReadAsStreamAsync()))!;

        if (string.IsNullOrWhiteSpace(userCredentialsDto.Username) ||
            string.IsNullOrWhiteSpace(userCredentialsDto.Password))
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.BadRequest, "Username and password are required.");
        }

        //DB call
        UserCredentialsDto? databaseUserCredentials =
            await RepositoryManager.UserCredentialsRepository.GetByIdAsync(userCredentialsDto.Username);

        if (databaseUserCredentials == null)
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.NotFound, "User does not exist.");
        }

        if (!PasswordUtilities.VerifyPassword(userCredentialsDto.Password, databaseUserCredentials.Password))
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.Unauthorized, "Password does not match.");
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