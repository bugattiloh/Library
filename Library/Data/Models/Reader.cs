using Library.Data.Models;

namespace Library;

public class Reader
{
    public int Id { get; set; }
    public string FIO { get; set; }
    
    public DateTime Birthdate { get; set; }
    
    public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();
}