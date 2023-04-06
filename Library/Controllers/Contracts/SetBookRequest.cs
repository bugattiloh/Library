namespace Library.Controllers.Contracts;

public class SetBookRequest
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public string Author { get; set; }

    public string Article { get; set; }

    public DateTime CreatedAt { get; set; }

    public int Count { get; set; }
}