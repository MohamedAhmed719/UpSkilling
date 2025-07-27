using Task.Api.Contracts.Books;

namespace Task.Api.Services;

public interface IBookService
{
    Task<Result<BookResponse>> AddAsync(BookRequest request, CancellationToken cancellationToken = default!);
    Task<Result<BookResponse>> GetAsync(int id, CancellationToken cancellationToken = default!);
    Task<IEnumerable<BookResponse>> GetAllAsync(CancellationToken cancellationToken = default!);
    Task<Result> UpdateAsync(int id, BookRequest request, CancellationToken cancellationToken = default!);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default!);
}
