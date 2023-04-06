using Library.Controllers.Contracts;

namespace Library.Services;

public interface IReaderService
{
    Task<int> Add(Reader reader, CancellationToken ct);
    
    Task Remove(int readerId, CancellationToken ct);
    
    Task Update(SetReaderRequest request, CancellationToken ct);
    
    Task<GetReaderResponse> GetById(int readerId, CancellationToken ct);
    
    Task GetBook(GetOrReturnBookForReaderRequest request, CancellationToken ct);
    
    Task ReturnBook(GetOrReturnBookForReaderRequest request, CancellationToken ct);
    
    Task<IReadOnlyCollection<GetReaderResponse>> SearchReader(SearchReaderRequest request, CancellationToken ct);
}