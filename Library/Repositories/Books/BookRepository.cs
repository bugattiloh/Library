using Library.Controllers.Contracts;
using Library.Data;
using Library.Exceptions;
using Library.Repositories.Books;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Book;

public class BookRepository : IBookRepository
{
    private readonly LibraryContext _context;

    public BookRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<int> Add(Data.Models.Book book, CancellationToken ct)
    {
        await _context.Books.AddAsync(book, ct);
        await _context.SaveChangesAsync(ct);
        return book.Id;
    }

    public async Task Remove(int bookId, CancellationToken ct)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == bookId, cancellationToken: ct);
        if (book == null)
        {
            throw new BusinessLogicException("Книга не найдена");
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync(ct);
    }

    public async Task Update(Data.Models.Book book, CancellationToken ct)
    {
        var dbbook = await _context.Books.FirstOrDefaultAsync(x => x.Id == book.Id, cancellationToken: ct);
        if (dbbook == null)
        {
            throw new BusinessLogicException("Книга не найдена");
        }

        dbbook.Article = book.Article;
        dbbook.Author = book.Author;
        dbbook.Name = book.Name;
        dbbook.Count = book.Count;
        dbbook.CreatedAt = book.CreatedAt;
        await _context.SaveChangesAsync(ct);
    }

    public async Task<Data.Models.Book> GetById(int bookId, CancellationToken ct)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == bookId, cancellationToken: ct);
        if (book == null)
        {
            throw new BusinessLogicException("Книга не найдена");
        }

        return book;
    }

    public async Task<IReadOnlyCollection<Data.Models.Book>> GetByName(string name, CancellationToken ct)
    {
        return await _context.Books.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToListAsync(cancellationToken: ct);
    }

    public async Task<IReadOnlyCollection<Data.Models.Book>> GetBooksInLibrary(CancellationToken ct)
    {
        return await _context.Books.Where(x => x.Count > 0).ToListAsync(cancellationToken: ct);
    }

    public  async Task<IReadOnlyCollection<GetBookNotInLibraryResponse>> GetBooksNotInLibrary(CancellationToken ct)
    {
        var readers = await _context.Readers.ToListAsync(cancellationToken: ct);

        return (from r in readers
            from x in r.Books
            select new GetBookNotInLibraryResponse()
            {
                Article = x.Article,
                CreatedAt = x.CreatedAt,
                Author = x.Author,
                Name = x.Name,
                Reader = r.FIO
            }).ToList();
    }
}