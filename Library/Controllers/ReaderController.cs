using Library.Controllers.Contracts;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ReaderController : ControllerBase
{
    private readonly IReaderService _readerService;

    public ReaderController(IReaderService readerService)
    {
        _readerService = readerService;
    }

    [HttpPost]
    public async Task<int> Add([FromBody] AddReaderRequest request, CancellationToken ct)
    {
        return await _readerService.Add(request.ToModel(), ct);
    }

    [HttpPost]
    public async Task Remove([FromBody] RemoveReaderRequest request, CancellationToken ct)
    {
        await _readerService.Remove(request.ReaderId, ct);
    }

    [HttpPost]
    public async Task Set([FromBody] SetReaderRequest request, CancellationToken ct)
    {
        await _readerService.Update(request, ct);
    }

    [HttpPost]
    public async Task<GetReaderResponse> GetById([FromBody] GetReaderByIdRequest request, CancellationToken ct)
    {
        return await _readerService.GetById(request.ReaderId, ct);
    }

    [HttpPost]
    public async Task GetBookForReader([FromBody] GetOrReturnBookForReaderRequest request, CancellationToken ct)
    {
        await _readerService.GetBook(request, ct);
    }

    [HttpPost]
    public async Task ReturnBookForReader([FromBody] GetOrReturnBookForReaderRequest request, CancellationToken ct)
    {
        await _readerService.ReturnBook(request, ct);
    }

    [HttpPost]
    public async Task<IReadOnlyCollection<GetReaderResponse>> SearchReadersByFio([FromBody] SearchReaderRequest request,
        CancellationToken ct)
    {
        return await _readerService.SearchReader(request, ct);
    }
}