using Library.Controllers.Contracts;

namespace Library.Services;

public static class Mapper
{
    public static GetBookResponse ToResponse(this Data.Models.Book book)
    {
        return new GetBookResponse()
        {
            Article = book.Article,
            Author = book.Author,
            Count = book.Count,
            Name = book.Name,
            CreatedAt = book.CreatedAt
        };
    }
    
    public static IReadOnlyCollection<GetBookResponse> ToResponse(this IReadOnlyCollection<Data.Models.Book> books)
    {
        var result = books.Select(x => x.ToResponse()).ToList();
        return result;
    }
    
    public static GetReaderResponse ToResponse(this Reader reader)
    {
        return new GetReaderResponse()
        {
            Books = reader.Books.ToResponse(),
            Birthdate = reader.Birthdate,
            FIO = reader.FIO
        };
    }
    
    public static ICollection<GetBookResponse> ToResponse(this ICollection<Data.Models.Book> books)
    {
        var result = books.Select(x => x.ToResponse()).ToList();
        return result;
    }
    
    public static Reader ToModel(this AddReaderRequest request)
    {
        return new Reader()
        {
            Birthdate = request.Birthdate,
            FIO = request.FIO
        };
    }
}