using System;
using System.Collections.Generic;
using BookBird.Infrastructure.EF.Models;

namespace BookBird.Infrastructure.EF.SeedData
{
    internal static class AuthorSeed
    {
        public static IEnumerable<AuthorReadModel> GetSeed()
        {
            var createdAt = new DateTime(2023, 1, 1, 12, 00, 00);
            
            var walterIsaacson = new AuthorReadModel
            {
                Id = SeedIds.WalterIsaacsonId,
                FirstName = "Walter",
                LastName = "Isaacson",
                CreatedAt = createdAt
            };
            
            var danBrown = new AuthorReadModel
            {
                Id = SeedIds.DanBrownId,
                FirstName = "Dan",
                LastName = "Brown",
                CreatedAt = createdAt
            };
            
            var danielWhiteson = new AuthorReadModel
            {
                Id = SeedIds.DanielWhitesonId,
                FirstName = "Daniel",
                LastName = "Whiteson",
                CreatedAt = createdAt
            };

            return new[] {walterIsaacson, danBrown, danielWhiteson};
        }
    }
}