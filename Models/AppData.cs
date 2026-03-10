namespace HttpPoster.Models;

public class AppData
{
    public List<SavedRequest> Favorites { get; set; } = [];
    public List<HistoryEntry> History { get; set; } = [];
}
