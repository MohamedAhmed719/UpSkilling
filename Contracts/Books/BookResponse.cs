namespace Task.Api.Contracts.Books;

public record BookResponse(
       int Id,
       string Name,
       string Description,
       string Author,
       int Price,
       int Stock,
       int CategoryId
    );
