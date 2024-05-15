using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NOS.Engineering.Challenge.API.Models;
using NOS.Engineering.Challenge.Managers;
using NOS.Engineering.Challenge.Models;

namespace NOS.Engineering.Challenge.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ContentController : Controller
{
    private readonly IContentsManager _manager;
    private readonly ILogger<Content> _logger;

    public ContentController(IContentsManager manager, ILogger<Content> logger)
    {
        _manager = manager;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetManyContents()
    {
        _logger.Log(LogLevel.Information, "GetManyContents");

        try
        {
            var contents = await _manager.GetManyContents().ConfigureAwait(false);

            if (!contents.Any())
                return NotFound();

            return Ok(contents);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"ERROR: {ex.Message}");

            return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Server Error. Contact your system administrador.");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContent(Guid id)
    {
        _logger.Log(LogLevel.Information, "GetContent");

        try
        {
            var content = await _manager.GetContent(id).ConfigureAwait(false);

            if (content == null)
                return NotFound();

            return Ok(content);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"ERROR: {ex.Message}");

            return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Server Error. Contact your system administrador.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateContent(
        [FromBody] ContentInput content
        )
    {
        _logger.Log(LogLevel.Information, "CreateContent");

        try
        {
            var createdContent = await _manager.CreateContent(content.ToDto()).ConfigureAwait(false);

            return createdContent == null ? Problem() : Ok(createdContent);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"ERROR: {ex.Message}");

            return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Server Error. Contact your system administrador.");
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateContent(
        Guid id,
        [FromBody] ContentInput content
        )
    {
        _logger.Log(LogLevel.Information, "UpdateContent");

        try
        {
            var updatedContent = await _manager.UpdateContent(id, content.ToDto()).ConfigureAwait(false);

            return updatedContent == null ? NotFound() : Ok(updatedContent);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"ERROR: {ex.Message}");

            return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Server Error. Contact your system administrador.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContent(
        Guid id
    )
    {
        _logger.Log(LogLevel.Information, "DeleteContent");

        try
        {
            var deletedId = await _manager.DeleteContent(id).ConfigureAwait(false);
            return Ok(deletedId);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"ERROR: {ex.Message}");

            return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Server Error. Contact your system administrador.");
        }
    }

    [HttpPost("{id}/genre")]
    public async Task<IActionResult> AddGenres(
        Guid id,
        [FromBody] IEnumerable<string> genres
    )
    {
        _logger.Log(LogLevel.Information, "AddGenres");

        try
        {
            var content = await _manager.GetContent(id).ConfigureAwait(false);

            if (content != null && await _manager.CheckIfGenreExists(content, genres))
                return StatusCode((int)HttpStatusCode.BadRequest, "The genre already exists.");

            var genresList = await _manager.AddGenreToContent(content.GenreList.ToList(), genres.ToList());
            var updatedContent = await _manager.UpdateContent(id, new ContentGenresInput().ToDto(genresList)).ConfigureAwait(false);

            return updatedContent == null ? NotFound() : Ok(updatedContent);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"ERROR: {ex.Message}");

            return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Server Error. Contact your system administrador.");
        }
    }

    [HttpDelete("{id}/genre")]
    public async Task<IActionResult> RemoveGenres(
        Guid id,
        [FromBody] IEnumerable<string> genres
    )
    {
        _logger.Log(LogLevel.Information, "RemoveGenres");

        try
        {
            var content = await _manager.GetContent(id).ConfigureAwait(false);
            var genresList = await _manager.RemoveGenreToContent(content.GenreList.ToList(), genres.ToList());
            var updatedContent = await _manager.UpdateContent(id, new ContentGenresInput().ToDto(genresList)).ConfigureAwait(false);

            return updatedContent == null ? NotFound() : Ok(updatedContent);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"ERROR: {ex.Message}");

            return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Server Error. Contact your system administrador.");
        }
    }

    /// <summary>
    /// Search Contents by startTime, endTime and term. The field term filters the contents by Title, SubTitle, Description and Genres.
    /// </summary>
    [HttpGet("Search")]
    public async Task<IActionResult> GetManyContentsByCriteria([FromQuery, BindRequired] string term, [FromQuery] DateTime? startTime, [FromQuery] DateTime? endTime)
    {
        _logger.Log(LogLevel.Information, "GetManyContentsByCriteria");

        try
        {
            var contents = await _manager.GetManyContentsByCriteria(term, startTime, endTime).ConfigureAwait(false);

            if (!contents.Any())
                return NotFound();

            return Ok(contents);
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"ERROR: {ex.Message}");

            return StatusCode((int)HttpStatusCode.InternalServerError, "Internal Server Error. Contact your system administrador.");
        }
    }
}