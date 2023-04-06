using Library.Controllers.Contracts;

namespace Library.Services.Books;

public interface IBookService
{
    Task<int> Add(AddBookRequest request, CancellationToken ct);
    
    Task Remove(RemoveBookRequest request, CancellationToken ct);
    
    Task Update(SetBookRequest request, CancellationToken ct);
    
    Task<GetBookResponse> GetById(GetBookByIdRequest request, CancellationToken ct);
    
    Task<IReadOnlyCollection<GetBookResponse>> GetByName(GetBookByNameRequest request, CancellationToken ct);
    
    Task<IReadOnlyCollection<GetBookResponse>> GetBooksInLibrary( CancellationToken ct);
    
    Task<IReadOnlyCollection<GetBookNotInLibraryResponse>> GetBooksNotInLibrary( CancellationToken ct);
}