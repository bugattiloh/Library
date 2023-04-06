namespace Library.Controllers.Contracts;

public class GetOrReturnBookForReaderRequest
{
    public int ReaderId { get; set; }
    public int BookId { get; set; }
}