using System;
using System.Collections.Generic;
using BookBird.Infrastructure.EF.Models;

namespace BookBird.Infrastructure.EF.SeedData
{
    internal static class BookSeed
    {
        public static IEnumerable<BookReadModel> GetSeed()
        {
            var createdAt = new DateTime(2023, 1, 1, 12, 00, 00);
            
            var einsteinLife = new BookReadModel
            {
                Id = SeedIds.EinsteinLifeId,
                Name = "Einstein: His Life and Universe",
                PublishYear = 2007,
                AuthorId = SeedIds.WalterIsaacsonId,
                CreatedAt = createdAt
            };

            var franklinLife = new BookReadModel
            {
                Id = SeedIds.FranklinLifeId,
                Name = "Benjamin Franklin: An American Life",
                PublishYear = 2003,
                AuthorId = SeedIds.WalterIsaacsonId,
                CreatedAt = createdAt
            };

            var daVinciLife = new BookReadModel
            {
                Id = SeedIds.DaVinciLifeId,
                Name = "Leonardo da Vinci",
                PublishYear = 2017,
                AuthorId = SeedIds.WalterIsaacsonId,
                CreatedAt = createdAt
            };
            
            var angelsAndDemons = new BookReadModel
            {
                Id = SeedIds.AngelAndDemonsId,
                Name = "Angels & Demons",
                PublishYear = 2000,
                AuthorId = SeedIds.DanBrownId,
                CreatedAt = createdAt
            };
            
            var daVinciCode = new BookReadModel
            {
                Id = SeedIds.DaVinciCodeId,
                Name = "The Da Vinci Code",
                PublishYear = 2003,
                AuthorId = SeedIds.DanBrownId,
                CreatedAt = createdAt
            };
            
            var unknownUniverse = new BookReadModel
            {
                Id = SeedIds.UnknownUniverseId,
                Name = "We Have No Idea: A Guide to the Unknown Universe",
                PublishYear = 2017,
                AuthorId = SeedIds.DanielWhitesonId,
                CreatedAt = createdAt
            };

            return new[] {einsteinLife, franklinLife, daVinciLife, angelsAndDemons, daVinciCode, unknownUniverse};
        }
    }
}