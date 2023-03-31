using System;
using System.Collections.Generic;

namespace BookBird.Infrastructure.EF.SeedData
{
    internal static class BooksGenresSeed
    {
        public static IEnumerable<BooksGenres> GetSeed()
        {
            return new []
            {
                new BooksGenres {BookId = SeedIds.EinsteinLifeId, GenreId = SeedIds.BiographyId},
                new BooksGenres {BookId = SeedIds.EinsteinLifeId, GenreId = SeedIds.NonfictionId},
                new BooksGenres {BookId = SeedIds.FranklinLifeId, GenreId = SeedIds.BiographyId},
                new BooksGenres {BookId = SeedIds.FranklinLifeId, GenreId = SeedIds.NonfictionId},
                new BooksGenres {BookId = SeedIds.DaVinciLifeId, GenreId = SeedIds.BiographyId},
                new BooksGenres {BookId = SeedIds.DaVinciLifeId, GenreId = SeedIds.NonfictionId},
                new BooksGenres {BookId = SeedIds.AngelAndDemonsId, GenreId = SeedIds.FictionId},
                new BooksGenres {BookId = SeedIds.AngelAndDemonsId, GenreId = SeedIds.ThrillerId},
                new BooksGenres {BookId = SeedIds.AngelAndDemonsId, GenreId = SeedIds.AdventureId},
                new BooksGenres {BookId = SeedIds.DaVinciCodeId, GenreId = SeedIds.FictionId},
                new BooksGenres {BookId = SeedIds.DaVinciCodeId, GenreId = SeedIds.ThrillerId},
                new BooksGenres {BookId = SeedIds.DaVinciCodeId, GenreId = SeedIds.AdventureId},
                new BooksGenres {BookId = SeedIds.UnknownUniverseId, GenreId = SeedIds.NonfictionId}
            };
        }
    }

    internal class BooksGenres
    {
        public Guid BookId { get; set; }
        public Guid GenreId { get; set; }
    }
}