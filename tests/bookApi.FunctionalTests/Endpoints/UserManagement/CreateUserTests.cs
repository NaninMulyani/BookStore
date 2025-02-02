﻿using Bogus;
using bookApi.Domain.Extensions;
using bookApi.FunctionalTests.Helpers;
using bookApi.WebApi.Contracts.Responses;
using bookApi.WebApi.Endpoints.UserManagement.Requests;
using System.Net;
using Xunit.Abstractions;

namespace bookApi.FunctionalTests.Endpoints.UserManagement;

public class CreateUserTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    private static readonly Guid[] Roles = { RoleExtensions.SuperAdministratorId };

    private readonly Faker<CreateUserRequest> _createUserRequestGenerator =
        new Faker<CreateUserRequest>()
            .RuleFor(e => e.Username, f => f.Person.UserName)
            .RuleFor(e => e.Password, f => f.Internet.Password())
            .RuleFor(e => e.Fullname, f => f.Person.FullName)
            .RuleFor(e => e.EmailAddress, f => f.Person.Email)
            .RuleFor(e => e.RoleId, f => f.PickRandom(Roles))
            .UseSeed(50);

    private readonly LoginResponse _adminAuth;

    public CreateUserTests(CustomWebApplicationFactory waf, ITestOutputHelper testOutputHelper)
    {
        waf.SetOutPut(testOutputHelper);
        _client = waf.CreateClient();
        _adminAuth = _client.LoginAsync("admin", "Qwerty@1234").GetAwaiter().GetResult();
    }

    [Fact]
    public async Task CreateUser_Should_CreateUser_Also_Return_NoContent()
    {
        const string path = "api/user-management/users";

        var request = HttpRequestMessageExtensions.Create(HttpMethod.Post,
            _client.BaseAddress!,
            path,
            HttpRequestMessageExtensions.CreateJsonAsContent(_createUserRequestGenerator.Generate()),
            new Dictionary<string, string>
            {
                { "Authorization", $"bearer {_adminAuth.AccessToken}" }
            });

        // Act
        var response = await _client.SendAsync(request);
        var responseInString = await response.Content.ReadAsStringAsync();
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent, responseInString);
    }
}