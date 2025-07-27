namespace Task.Api.Contracts.Books;

public record BookRequest(
       string Name,
       string Description,
       string Author,
       int Price,
       int Stock,
       int CategoryId
    );