﻿using bookApi.Core.Abstractions;
using bookApi.Shared.Abstractions.Databases;
using bookApi.WebApi.Endpoints.UserManagement;
using bookApi.WebApi.Endpoints.UserManagement.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Xunit.Abstractions;

namespace bookApi.IntegrationTests.Endpoints.UserManagement;

[Collection(nameof(UserManagementFixture))]
public class UpdateUserTests
{
    private readonly UserManagementFixture _fixture;

    public UpdateUserTests(UserManagementFixture fixture, ITestOutputHelper testOutputHelper)
    {
        fixture.SetOutput(testOutputHelper);
        fixture.ConstructFixture();
        _fixture = fixture;
    }

    [Fact]
    public async Task UpdateUser_Given_CorrectRequest_ShouldReturn_Ok()
    {
        // Arrange
        using var scope = _fixture.ServiceProvider.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        var user = await userService.GetByIdAsync(Guid.Empty, CancellationToken.None);
        user!.FullName.ShouldBe("Super Administrator"); // default value

        var updateUser = new UpdateUser(
            scope.ServiceProvider.GetRequiredService<IUserService>(),
            scope.ServiceProvider.GetRequiredService<IDbContext>(),
            scope.ServiceProvider.GetRequiredService<IStringLocalizer<UpdateUser>>());

        const string newFullName = "Test1234";
        // Act
        var result = await updateUser.HandleAsync(new UpdateUserRequest
        {
            UserId = Guid.Empty,
            UpdateUserRequestPayload = new UpdateUserRequestPayload() { Fullname = newFullName }
        });

        // Assert the expected results
        result.ShouldNotBeNull();
        result.ShouldBeOfType(typeof(NoContentResult));
        user = await userService.GetByIdAsync(Guid.Empty, CancellationToken.None);
        user!.FullName.ShouldBe(newFullName);
    }
}