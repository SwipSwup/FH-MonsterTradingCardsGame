using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Server.Repositories;
using Server.Utilities;
using Shared.DTOs;
using Shared.DTOs.Authentication;
using Shared.Networking.Http;

namespace Server.Controller;

public static class UserController
{
    public static async Task<HttpResponseMessage> UpdateUsernameAsync(HttpRequestMessage request)
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
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.BadRequest, "Expected a JSON body but none was provided.");
        }

        UserDto userDto =
            (await JsonSerializer.DeserializeAsync<UserDto>(await request.Content!.ReadAsStreamAsync()))!;

        if (string.IsNullOrWhiteSpace(userDto.Username))
           
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.BadRequest, "Username is required.");
        }

        UserDto? user = await RepositoryManager.UserRepository.GetByIdAsync(userId);
            
        if (user == null)
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.NotFound,
                "The user with the provided ID or username does not exist.");
        }

        user.Username = userDto.Username;
        
        await RepositoryManager.UserRepository.UpdateAsync(user);
        
        return HttpUtilities.GenerateResponse(HttpStatusCode.OK, "Username updated.");
    }
    
    public static async Task<HttpResponseMessage> RegisterUserAsync(HttpRequestMessage request)
    {
        if (Equals(request.Content?.Headers.ContentType, MediaTypeHeaderValue.Parse("application/json")))
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.UnsupportedMediaType,
                "Content-Type must be application/json");
        }
        
        if (request.Content == null)
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.BadRequest, "Expected a JSON body but none was provided.");
        }

        UserDto userDto =
            (await JsonSerializer.DeserializeAsync<UserDto>(await request.Content!.ReadAsStreamAsync()))!;

        if (string.IsNullOrWhiteSpace(userDto.Username) ||
            string.IsNullOrWhiteSpace(userDto.Password))
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.BadRequest, "Username and Password are required.");
        }

        UserDto? dbUserDto =
            await RepositoryManager.UserRepository.GetByUsernameAsync(userDto.Username);
        if (dbUserDto != null)
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.Conflict, "Username already exists.");
        }

        userDto.Password = PasswordUtilities.HashPassword(userDto.Password);
        await RepositoryManager.UserRepository.AddAsync(userDto);

        AuthenticationResponseDto responseDto = new AuthenticationResponseDto
        {
            Success = true,
            Message = "Registration Success",
            Token = TokenUtilities.GenerateJwtToken(userDto)
        };

        return HttpUtilities.GenerateResponse(HttpStatusCode.Created, JsonSerializer.Serialize(responseDto));
    }

    public static async Task<HttpResponseMessage> LoginUserAsync(HttpRequestMessage request)
    {
        if (Equals(request.Content?.Headers.ContentType, MediaTypeHeaderValue.Parse("application/json")))
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.UnsupportedMediaType,"Content-Type must be application/json");
        }

        UserDto userDto =
            (await JsonSerializer.DeserializeAsync<UserDto>(await request.Content!.ReadAsStreamAsync()))!;

        if (string.IsNullOrWhiteSpace(userDto.Username) ||
            string.IsNullOrWhiteSpace(userDto.Password))
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.BadRequest, "Username and password are required.");
        }

        UserDto? dbUserDto =
            await RepositoryManager.UserRepository.GetByUsernameAsync(userDto.Username);

        if (dbUserDto == null)
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.NotFound, "User does not exist.");
        }

        if (!PasswordUtilities.VerifyPassword(userDto.Password, dbUserDto.Password))
        {
            return HttpUtilities.GenerateErrorResponse(HttpStatusCode.Unauthorized, "Password does not match.");
        }

        AuthenticationResponseDto responseDto = new AuthenticationResponseDto
        {
            Success = true,
            Message = "Login Success",
            Token = TokenUtilities.GenerateJwtToken(userDto)
        };

        return HttpUtilities.GenerateResponse(HttpStatusCode.OK, JsonSerializer.Serialize(responseDto));
    }
}