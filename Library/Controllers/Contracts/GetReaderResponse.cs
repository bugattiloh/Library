using Library.Data.Models;

namespace Library.Controllers.Contracts;

public class GetReaderResponse
{
    public string FIO { get; set; }

    public DateTime Birthdate { get; set; }
    
    public ICollection<GetBookResponse> Books { get; set; }
}