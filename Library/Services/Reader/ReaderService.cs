using Library.Controllers.Contracts;
using Library.Repository;

namespace Library.Services;

public class ReaderService : IReaderService
{
    private readonly IReaderRepository _readerRepository;

    public ReaderService(IReaderRepository readerRepository)
    {
        _readerRepository = readerRepository;
    }

    public async Task<int> Add(Reader reader, CancellationToken ct)
    {
        return await _readerRepository.Add(reader, ct);
    }

    public async Task Remove(int readerId, CancellationToken ct)
    {
        await _readerRepository.Remove(readerId, ct);
    }

    public async Task Update(SetReaderRequest request, CancellationToken ct)
    {
        await _readerRepository.Update(request, ct);
    }

    public async Task<GetReaderResponse> GetById(int readerId, CancellationToken ct)
    {
        var reader = await _readerRepository.GetById(readerId, ct);
        return reader.ToResponse();
    }

    public async Task GetBook(GetOrReturnBookForReaderRequest request, CancellationToken ct)
    {
        await _readerRepository.GetBook(request, ct);
    }

    public async Task ReturnBook(GetOrReturnBookForReaderRequest request, CancellationToken ct)
    {
        await _readerRepository.ReturnBook(request, ct);
    }

    public async Task<IReadOnlyCollection<GetReaderResponse>> SearchReader(SearchReaderRequest request,
        CancellationToken ct)
    {
        var readers = await _readerRepository.SearchReader(request, ct);
        var response = readers.Select(x => new GetReaderResponse()
        {
            Books = x.Books.ToResponse(),
            Birthdate = x.Birthdate,
            FIO = x.FIO
        }).ToList();
        return response;
    }
}