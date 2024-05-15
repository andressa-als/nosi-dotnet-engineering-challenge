using NOS.Engineering.Challenge.Database;
using NOS.Engineering.Challenge.Models;

namespace NOS.Engineering.Challenge.Managers;

public class ContentsManager : IContentsManager
{
    private readonly IDatabase<Content?, ContentDto> _database;

    public ContentsManager(IDatabase<Content?, ContentDto> database)
    {
        _database = database;
    }

    public async Task<IEnumerable<Content?>> GetManyContents()
    {
        try
        {
            return await _database.ReadAll();
        }
        catch
        {
            throw;
        }
    }

    public async Task<Content?> CreateContent(ContentDto content)
    {
        try
        {
            return await _database.Create(content);
        }
        catch
        {
            throw;
        }
    }

    public async Task<Content?> GetContent(Guid id)
    {
        try
        {
            return await _database.Read(id);
        }
        catch
        {
            throw;
        }
    }

    public async Task<Content?> UpdateContent(Guid id, ContentDto content)
    {
        try
        {
            return await _database.Update(id, content);
        }
        catch
        {
            throw;
        }
    }

    public async Task<Guid> DeleteContent(Guid id)
    {
        try
        {
            return await _database.Delete(id);
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> CheckIfGenreExists(Content content, IEnumerable<string> GenreList)
    {
        try
        {
            if (content.GenreList.Any() &&
                !content.GenreList.Where(g => GenreList.Contains(g)).Any())
                return false;
            else
                return true;
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<string>> AddGenreToContent(List<string> Genres, List<string> NewGenres)
    {
        try
        {
            Genres.AddRange(NewGenres);

            return Genres;
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<string>> RemoveGenreToContent(List<string> Genres, List<string> GenresToDelete)
    {
        try
        {
            GenresToDelete.ForEach(g => Genres.Remove(g));

            return Genres;
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Content?>> GetManyContentsByCriteria(string term, DateTime? startTime, DateTime? endTime)
    {
        try
        {
            return _database.ReadAll().Result.Where(c => c.Title.Contains(term) || c.SubTitle.Contains(term) ||
                                                            c.Description.Contains(term) || c.GenreList.Contains(term) &&
                                                            c.StartTime >= (startTime != null ? (startTime) : c.StartTime) &&
                                                            c.EndTime >= (endTime != null ? (endTime) : c.EndTime));
        }
        catch
        {
            throw;
        }
    }
}