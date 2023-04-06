using Library.Controllers.Contracts;

namespace Library.Repository;

public interface IReaderRepository
{
    Task<int> Add(Reader reader, CancellationToken ct);
    
    Task Remove(int readerId, CancellationToken ct);
    
    Task Update(SetReaderRequest request, CancellationToken ct);
    
    Task<Reader> GetById(int readerId, CancellationToken ct);
    
    Task GetBook(GetOrReturnBookForReaderRequest request, CancellationToken ct);
    
    Task ReturnBook(GetOrReturnBookForReaderRequest request, CancellationToken ct);
    
    Task<IReadOnlyCollection<Reader>> SearchReader(SearchReaderRequest request, CancellationToken ct);
}