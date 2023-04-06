using Library.Controllers.Contracts;
using Library.Data.Models;

namespace Library.Repositories.Books;

public interface IBookRepository
{
    Task<int> Add(Book book, CancellationToken ct);
    
    Task Remove(int bookId, CancellationToken ct);
    
    Task Update(Book book, CancellationToken ct);
    
    Task<Book> GetById(int bookId, CancellationToken ct);
    
    Task<IReadOnlyCollection<Book>> GetByName(string name, CancellationToken ct);
    
    Task<IReadOnlyCollection<Book>> GetBooksInLibrary( CancellationToken ct);
    
    Task<IReadOnlyCollection<GetBookNotInLibraryResponse>> GetBooksNotInLibrary( CancellationToken ct);
}