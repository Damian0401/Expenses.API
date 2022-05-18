using Application.Interfaces;
using AutoMapper;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Presistence;

namespace Application.Services
{
    public class Service
    {
        protected DataContext Context { get; } = null!;
        protected ApplicationUser? CurrentlyLoggedUser { get; set; }
        protected IMapper Mapper { get; } = null!;
        protected UserManager<ApplicationUser> UserManager { get; }
        protected string? CurrentlyLoggedUserId { get; }

        public Service(IServiceProvider serviceProvider)
        {
            Context = serviceProvider.GetService<DataContext>()!;

            UserManager = serviceProvider.GetService<UserManager<ApplicationUser>>()!;

            Mapper = serviceProvider.GetService<IMapper>()!;

            var userAccessor = serviceProvider.GetService<IUserAccessor>();

            CurrentlyLoggedUserId = userAccessor!.GetCurrentlyLoggedUserId();

            AssignCurrentlyLoggedUser();
        }

        private void AssignCurrentlyLoggedUser()
        {
            if (CurrentlyLoggedUserId is null)
            {
                CurrentlyLoggedUser = null;
                return;
            }

            CurrentlyLoggedUser = UserManager.FindByIdAsync(CurrentlyLoggedUserId).Result;
        }

    }
}