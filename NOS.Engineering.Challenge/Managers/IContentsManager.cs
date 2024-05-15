using NOS.Engineering.Challenge.Models;

namespace NOS.Engineering.Challenge.Managers;

public interface IContentsManager
{
    Task<IEnumerable<Content?>> GetManyContents();
    Task<Content?> CreateContent(ContentDto content);
    Task<Content?> GetContent(Guid id);
    Task<Content?> UpdateContent(Guid id, ContentDto content);
    Task<Guid> DeleteContent(Guid id);
    Task<bool> CheckIfGenreExists(Content content, IEnumerable<string> GenreList);
    Task<IEnumerable<string>> AddGenreToContent(List<string> Genres, List<string> NewGenres);
    Task<IEnumerable<string>> RemoveGenreToContent(List<string> Genres, List<string> GenresToDelete);
    Task<IEnumerable<Content?>> GetManyContentsByCriteria(string term, DateTime? startTime, DateTime? endTime);
}