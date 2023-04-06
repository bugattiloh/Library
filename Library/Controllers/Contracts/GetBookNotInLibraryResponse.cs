namespace Library.Controllers.Contracts;

public class GetBookNotInLibraryResponse
{
    public string Name { get; set; }

    public string Author { get; set; }

    public string Article { get; set; }
    public int Count { get; set; }
    
    public string Reader { get; set; }

    public DateTime CreatedAt { get; set; }
}