using BookBird.Application.CommandHandlers.Books;
using BookBird.Domain.Factories.Authors;
using BookBird.Domain.Factories.Books;
using BookBird.Domain.Factories.Feedbacks;
using BookBird.Domain.Factories.Genres;
using BookBird.Domain.Factories.Invitations;
using BookBird.Domain.Factories.Meetings;
using BookBird.Domain.Factories.Users;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookBird.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAuthorFactory, AuthorFactory>();
            services.AddSingleton<IGenreFactory, GenreFactory>();
            services.AddSingleton<IBookFactory, BookFactory>();
            services.AddSingleton<IFeedbackFactory, FeedbackFactory>();
            services.AddSingleton<IMeetingFactory, MeetingFactory>();
            services.AddSingleton<IUserFactory, UserFactory>();
            services.AddSingleton<IInvitationFactory, InvitationFactory>();

            services.AddMediatR(typeof(CreateBookHandler));

            return services;
        }
    }
}