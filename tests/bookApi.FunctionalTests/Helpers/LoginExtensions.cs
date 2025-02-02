﻿using bookApi.WebApi.Contracts.Responses;
using bookApi.WebApi.Endpoints.Identity.Requests;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace bookApi.FunctionalTests.Helpers;

public static class LoginExtensions
{
    public static async Task<LoginResponse> LoginAsync(this HttpClient client, string username, string password)
    {
        var path = "api/identity/sign-in";

        var request = HttpRequestMessageExtensions.Create(HttpMethod.Post,
            client.BaseAddress!,
            path,
            HttpRequestMessageExtensions.CreateJsonAsContent(new SignInRequest
            {
                Username = username,
                Password = password
            }));

        // Act
        var response = await client.SendAsync(request);
        var responseInString = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new Exception(responseInString);
        var vm = JsonSerializer.Deserialize<LoginResponse>(responseInString, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return vm!;
    }
}