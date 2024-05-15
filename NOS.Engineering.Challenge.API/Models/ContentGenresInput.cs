using NOS.Engineering.Challenge.Models;

namespace NOS.Engineering.Challenge.API.Models
{
    public class ContentGenresInput : ContentInput
    {
        public ContentDto ToDto(IEnumerable<string> Genres)
        {
            return new ContentDto(
                Title,
                SubTitle,
                Description,
                ImageUrl,
                Duration,
                StartTime,
                EndTime,
                Genres
            );
        }
    }
}
