using Application.Dtos.Bill.Requests;
using Application.Services;
using Application.Services.Utilities;
using Domain.Models;
using Domain.Models.Entities;
using Microsoft.Extensions.DependencyInjection;
using Presistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnitTests.Data.BillService;
using UnitTests.Helpers;
using Xunit;

namespace UnitTests;
public class BillServiceTests
{
    ServiceProviderHelper _helper;
    public BillServiceTests()
    {
        _helper = new ServiceProviderHelper();
    }

    [Fact]
    public async void ArchiveAllAsync_without_user_should_be_Unauthorized()
    {
        // Arrange
        var serviceProvider = _helper.GetServiceProvider(null);
        var billService = new BillService(serviceProvider);

        // Act
        var response = await billService.ArchiveAllAsync();

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async void CreateAsync_without_user_should_be_Unauthorized()
    {
        // Arrange
        var serviceProvider = _helper.GetServiceProvider(null);
        var billService = new BillService(serviceProvider);

        var billToCreate = new CreateBillDtoRequest
        {
            Description = "Bill to create",
            Value = 123.4
        };

        // Act
        var response = await billService.CreateAsync(billToCreate);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async void GetMyArchivedAsync_without_user_should_be_Unauthorized()
    {
        // Arrange
        var serviceProvider = _helper.GetServiceProvider(null);
        var billService = new BillService(serviceProvider);

        // Act
        var response = await billService.GetMyArchivedAsync();

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async void GetMyUnpaidAsync_without_user_should_be_Unauthorized()
    {
        // Arrange
        var serviceProvider = _helper.GetServiceProvider(null);
        var billService = new BillService(serviceProvider);

        // Act
        var response = await billService.GetMyUnpaidAsync();

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async void GetRoomArchivedAsync_without_user_should_be_Unauthorized()
    {
        // Arrange
        var serviceProvider = _helper.GetServiceProvider(null);
        var billService = new BillService(serviceProvider);

        // Act
        var response = await billService.GetRoomArchivedAsync();

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async void GetRoomUnpaidAsync_without_user_should_be_Unauthorized()
    {
        // Arrange
        var serviceProvider = _helper.GetServiceProvider(null);
        var billService = new BillService(serviceProvider);

        // Act
        var response = await billService.GetRoomUnpaidAsync();

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Theory]
    [ClassData(typeof(CreateAsyncTestData))]
    public async void CreateAsync_creates_data(List<CreateBillDtoRequest> billsToCreate,
        ApplicationUser owner)
    {
        // Arrange
        var serviceProvider = _helper.GetServiceProvider(owner);
        var billService = new BillService(serviceProvider);
        var context = serviceProvider.GetService<DataContext>()!;
        var responses = new List<ServiceResponse>();

        // Act
        foreach (var bill in billsToCreate)
        {
            var response = await billService.CreateAsync(bill);

            responses.Add(response);
        }

        var billCount = context.Bills
            .Count(x => x.Status.Equals(Status.Unpaid) && x.OwnerId.Equals(owner.Id));

        // Assert
        Assert.All(responses, r => r.StatusCode.Equals(HttpStatusCode.OK));
        Assert.Equal(billsToCreate.Count(), billCount);
    }
}