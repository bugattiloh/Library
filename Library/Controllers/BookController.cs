using Library.Services.Books;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers.Contracts;

[ApiController]
[Route("[controller]/[action]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost]
    public async Task<int> Add([FromBody] AddBookRequest request, CancellationToken ct)
    {
        return await _bookService.Add(request, ct);
    }

    [HttpPost]
    public async Task Remove([FromBody] RemoveBookRequest request, CancellationToken ct)
    {
        await _bookService.Remove(request, ct);
    }

    [HttpPost]
    public async Task Set([FromBody] SetBookRequest request, CancellationToken ct)
    {
        await _bookService.Update(request, ct);
    }

    [HttpPost]
    public async Task<GetBookResponse> GetById([FromBody] GetBookByIdRequest request, CancellationToken ct)
    {
        return await _bookService.GetById(request, ct);
    }
    
    [HttpPost]
    public async Task<IReadOnlyCollection<GetBookResponse>> SearchByName([FromBody] GetBookByNameRequest request, CancellationToken ct)
    {
        return await _bookService.GetByName(request, ct);
    }

    [HttpPost]
    public async Task<IReadOnlyCollection<GetBookResponse>> GetBooksInLibrary(CancellationToken ct)
    {
        return await _bookService.GetBooksInLibrary(ct);
    }
}