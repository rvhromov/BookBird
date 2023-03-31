using System;
using System.Collections.Generic;
using BookBird.Infrastructure.EF.Models;

namespace BookBird.Infrastructure.EF.SeedData
{
    internal static class GenreSeed
    {
        public static IEnumerable<GenreReadModel> GetSeed()
        {
            var createdAt = new DateTime(2023, 1, 1, 12, 00, 00);
            
            var biography = new GenreReadModel
            {
                Id = SeedIds.BiographyId,
                Name = "Biography",
                Description = "A biography is a non-fictional account of a person's life. Biographies are written by an author who is not the subject/focus of the book.",
                CreatedAt = createdAt
            };

            var nonfiction = new GenreReadModel
            {
                Id = SeedIds.NonfictionId,
                Name = "Nonfiction",
                Description = "Nonfiction is an account or representation of a subject which is presented as fact. This presentation may be accurate or not; that is, it can give either a true or a false account of the subject in question.",
                CreatedAt = createdAt
            };

            var fiction = new GenreReadModel
            {
                Id = SeedIds.FictionId,
                Name = "Fiction",
                Description = "Fiction is the telling of stories which are not real. More specifically, fiction is an imaginative form of narrative, one of the four basic rhetorical modes.",
                CreatedAt = createdAt
            };

            var thriller = new GenreReadModel
            {
                Id = SeedIds.ThrillerId,
                Name = "Thriller",
                Description = "Thrillers are characterized by fast pacing, frequent action, and resourceful heroes who must thwart the plans of more-powerful and better-equipped villains. Literary devices such as suspense, red herrings and cliffhangers are used extensively.",
                CreatedAt = createdAt
            };

            var adventure = new GenreReadModel
            {
                Id = SeedIds.AdventureId,
                Name = "Adventure",
                Description = "Adventure fiction is a genre of fiction in which an adventure, an exciting undertaking involving risk and physical danger, forms the main storyline.",
                CreatedAt = createdAt
            };

            return new[] {biography, nonfiction, fiction, thriller, adventure};
        }
    }
}