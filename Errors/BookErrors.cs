namespace Task.Api.Errors;

public static class BookErrors
{
    public static Error BookNotFound = new Error("Book.BookNotFound", "there was no book with the given id");

}
