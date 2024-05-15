using NOS.Engineering.Challenge.Database;
using NOS.Engineering.Challenge.Models;

namespace NOS.Engineering.Challenge.Tests
{
    public class GenresTest
    {
        private SlowDatabase<Content, ContentDto> _context = new SlowDatabase<Content, ContentDto>(new ContentMapper(), new MockData());

        [Fact]
        public void CheckIfGenreNotExists()
        {
            var contents = _context.ReadAll();

            var Genres = new List<string>() { "Drama", "Mystery" };

            bool result = contents.Result.First().GenreList.Where(g => Genres.Contains(g)).Any();

            Assert.False(result);
        }

        [Fact]
        public void GetAll()
        {
            var contents = _context.ReadAll();

            Assert.NotNull(contents);
            Assert.True(contents.Result.Any());
        }

        [Fact]
        public void GetById()
        {
            var contents = _context.ReadAll();
            var content = _context.Read(_context.ReadAll().Result.First().Id);

            Assert.NotNull(content);
            Assert.Equal(contents.Result.First().Id, content.Result.Id);
        }

        [Fact]
        public void GetByTerm()
        {
            string term = "Drama";

            var contents = _context.ReadAll().Result.Where(c => c.Title.Contains(term) || c.SubTitle.Contains(term) ||
                                                            c.Description.Contains(term) || c.GenreList.Contains(term));
            Assert.True(contents.Any());
        }
    }
}
