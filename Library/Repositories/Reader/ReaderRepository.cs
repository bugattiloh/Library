using Library.Controllers.Contracts;
using Library.Data;
using Library.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class ReaderRepository : IReaderRepository
{
    private readonly LibraryContext _context;

    public ReaderRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<int> Add(Reader reader, CancellationToken ct)
    {
        await _context.Readers.AddAsync(reader, ct);
        await _context.SaveChangesAsync(ct);
        return reader.Id;
    }

    public async Task Remove(int readerId, CancellationToken ct)
    {
        var reader = await _context.Readers.FirstOrDefaultAsync(x => x.Id == readerId, cancellationToken: ct);
        if (reader == null)
        {
            throw new BusinessLogicException("Читатель не найден");
        }

        _context.Readers.Remove(reader);
        await _context.SaveChangesAsync(ct);
    }

    public async Task Update(SetReaderRequest request, CancellationToken ct)
    {
        var dbReader = await _context.Readers.FirstOrDefaultAsync(x => x.Id == request.ReaderId, cancellationToken: ct);
        if (dbReader == null)
        {
            throw new BusinessLogicException("Читатель не найден");
        }

        dbReader.FIO = request.FIO;
        dbReader.Birthdate = request.Birthdate;
        await _context.SaveChangesAsync(ct);
    }

    public async Task<Reader> GetById(int readerId, CancellationToken ct)
    {
        var reader = await _context.Readers
            .Include(x => x.Books)
            .FirstOrDefaultAsync(x => x.Id == readerId, cancellationToken: ct);
        if (reader == null)
        {
            throw new BusinessLogicException("Читатель не найден");
        }

        return reader;
    }

    public async Task GetBook(GetOrReturnBookForReaderRequest request, CancellationToken ct)
    {
        var reader = await _context.Readers
            .Include(x => x.Books)
            .FirstOrDefaultAsync(x => x.Id == request.ReaderId, cancellationToken: ct);

        if (reader == null)
        {
            throw new BusinessLogicException("Читатель не найден");
        }

        var book = await _context.Books
            .FirstOrDefaultAsync(x => x.Id == request.BookId, cancellationToken: ct);
        if (reader == null)
        {
            throw new BusinessLogicException("Книга не найдена");
        }

        if (book.Count == 0)
        {
            throw new BusinessLogicException("Все книги раздали");
        }

        reader.Books.Add(book);
        book.Count--;
        await _context.SaveChangesAsync(ct);
    }

    public async Task ReturnBook(GetOrReturnBookForReaderRequest request, CancellationToken ct)
    {
        var reader = await _context.Readers
            .Include(x => x.Books)
            .FirstOrDefaultAsync(x => x.Id == request.ReaderId, cancellationToken: ct);

        if (reader == null)
        {
            throw new BusinessLogicException("Читатель не найден");
        }

        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == request.BookId, cancellationToken: ct);
        if (reader == null)
        {
            throw new BusinessLogicException("Книга не найдена");
        }

        reader.Books.Remove(book);
        book.Count++;
        await _context.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyCollection<Reader>> SearchReader(SearchReaderRequest request, CancellationToken ct)
    {
        return await _context.Readers
            .Include(x => x.Books)
            .Where(x => x.FIO.ToLower().Contains(request.FIO.ToLower())).ToListAsync(cancellationToken: ct);
    }
}