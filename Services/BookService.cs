using Task.Api.Contracts.Books;

namespace Task.Api.Services;

public class BookService(ApplicationDbContext context) : IBookService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<BookResponse>> AddAsync(BookRequest request,CancellationToken cancellationToken=default!)
    {

        var isCategoryExists = await _context.Categories.AnyAsync(x => x.Id == request.CategoryId);

        if (!isCategoryExists)
            return Result.Failure<BookResponse>(CategoryErrors.CategoryNotFound);
        var book = request.Adapt<Book>();

        await _context.AddAsync(book);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(book.Adapt<BookResponse>());
    }

    public async Task<Result<BookResponse>> GetAsync(int id,CancellationToken cancellationToken=default!)
    {
        var book = await _context.Books.Where(x=>x.Id == id).ProjectToType<BookResponse>().FirstOrDefaultAsync(cancellationToken);

        if (book is null)
            return Result.Failure<BookResponse>(BookErrors.BookNotFound);

        return Result.Success(book);
    }

    public async Task<IEnumerable<BookResponse>> GetAllAsync(CancellationToken cancellationToken = default!)
    {
        var books = await _context.Books.ProjectToType<BookResponse>().ToListAsync();


        return books;
    }



    public async Task<Result> UpdateAsync(int id,BookRequest request,CancellationToken cancellationToken = default!)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (book is null)
            return Result.Failure(BookErrors.BookNotFound);

        var isCategoryExists = await _context.Categories.AnyAsync(x => x.Id == request.CategoryId, cancellationToken);

        if (!isCategoryExists)
            return Result.Failure(CategoryErrors.CategoryNotFound);

        book.Name = request.Name;

        book.Description = request.Description;
        book.Author = request.Author;
        book.Price = request.Price;
        book.Stock = request.Stock;

        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }


    public async Task<Result> DeleteAsync(int id,CancellationToken cancellationToken = default!)
    {
        var isBookExists = await _context.Books.AnyAsync(x => x.Id == id, cancellationToken);

        if (!isBookExists)
            return Result.Failure(BookErrors.BookNotFound);

        await _context.Books.Where(x => x.Id == id)
            .ExecuteDeleteAsync(cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
