using System;
using Presistence;
using Application.Infrastructure;
using AutoMapper;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Infrastructure.Security;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Helpers;
public class ServiceProviderHelper
{
    public IServiceProvider GetServiceProvider(ApplicationUser? currentlyLoggedUser = null)
    {
        var contextAccessor = CreateAccessor(currentlyLoggedUser?.Id);
        var userAccessor = new UserAccessor(contextAccessor);

        var userManager = CreateManager(currentlyLoggedUser);

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        serviceCollection.AddSingleton<IUserAccessor>(userAccessor);
        serviceCollection.AddSingleton(userManager);
        serviceCollection.AddDbContext<DataContext>(opt => 
            opt.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()), 
            ServiceLifetime.Scoped, ServiceLifetime.Scoped);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        if (currentlyLoggedUser is not null)
        {
            var context = serviceProvider.GetRequiredService<DataContext>();
            context.Users.Add(currentlyLoggedUser);
            context.SaveChanges();
        }

        return serviceProvider;
    }

    private IHttpContextAccessor CreateAccessor(string? currentlyLoggedUserId)
    {
        var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

        if (currentlyLoggedUserId is null)
        {
            mockHttpContextAccessor.Setup(x => x.HttpContext!.User).Returns((ClaimsPrincipal)null!);

            return mockHttpContextAccessor.Object;
        }

        var claims = new List<Claim>
        {
            new("UserId", currentlyLoggedUserId),
        };

        mockHttpContextAccessor.Setup(x => x.HttpContext!.User.Claims).Returns(claims);

        return mockHttpContextAccessor.Object;
    }

    private UserManager<ApplicationUser> CreateManager(ApplicationUser? currentlyLoggedUser)
    {
        var store = new Mock<IUserStore<ApplicationUser>>();
        var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object,
            null, null, null, null, null, null, null, null);

        if (currentlyLoggedUser is null)
        {
            mockUserManager.Setup(m => m.FindByIdAsync(It.IsAny<string>()).Result).Returns((ApplicationUser)null!);

            return mockUserManager.Object;
        }

        mockUserManager.Setup(m => m.FindByIdAsync(currentlyLoggedUser!.Id).Result).Returns(currentlyLoggedUser!);

        return mockUserManager.Object;
    }
}