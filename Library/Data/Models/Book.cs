namespace Library.Data.Models;

public class Book
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public string Author { get; set; }

    public string Article { get; set; }

    public DateTime CreatedAt { get; set; }

    public int Count { get; set; }

    public virtual ICollection<Reader> Readers { get; set; } = new HashSet<Reader>();
}