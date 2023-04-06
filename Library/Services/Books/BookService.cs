using Library.Controllers.Contracts;
using Library.Repositories.Books;

namespace Library.Services.Books;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<int> Add(AddBookRequest request, CancellationToken ct)
    {
        var book = new Data.Models.Book()
        {
            Article = request.Article,
            Author = request.Author,
            Count = request.Count,
            Name = request.Name,
            CreatedAt = request.CreatedAt
        };
        return await _bookRepository.Add(book, ct);
    }

    public async Task Remove(RemoveBookRequest request, CancellationToken ct)
    {
        await _bookRepository.Remove(request.BookId, ct);
    }

    public async Task Update(SetBookRequest request, CancellationToken ct)
    {
        var book = new Data.Models.Book()
        {
            Id = request.Id,
            Article = request.Article,
            Author = request.Author,
            Count = request.Count,
            Name = request.Name,
            CreatedAt = request.CreatedAt
        };
        await _bookRepository.Update(book, ct);
    }

    public async Task<GetBookResponse> GetById(GetBookByIdRequest request, CancellationToken ct)
    {
        var book = await _bookRepository.GetById(request.BookId, ct);
        return book.ToResponse();
        
    }

    public async Task<IReadOnlyCollection<GetBookResponse>> GetByName(GetBookByNameRequest request,
        CancellationToken ct)
    {
        var books =  await _bookRepository.GetByName(request.BookName,ct);
        var response = books.Select(book => new GetBookResponse()
        {
            Article = book.Article,
            Author = book.Author,
            Count = book.Count,
            Name = book.Name,
            CreatedAt = book.CreatedAt
        }).ToList();
        return response;
    }

    public async Task<IReadOnlyCollection<GetBookResponse>> GetBooksInLibrary(CancellationToken ct)
    {
        var books =  await _bookRepository.GetBooksInLibrary(ct);
        return books.ToResponse();
    }

    public async Task<IReadOnlyCollection<GetBookNotInLibraryResponse>> GetBooksNotInLibrary(CancellationToken ct)
    {
        var books =  await _bookRepository.GetBooksNotInLibrary(ct);
        var response = books.Select(book => new GetBookNotInLibraryResponse()
        {
            Article = book.Article,
            Author = book.Author,
            Count = book.Count,
            Name = book.Name,
            CreatedAt = book.CreatedAt
        }).ToList();
        
        return response;
    }
}