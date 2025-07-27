namespace Task.Api.Errors;

public static class CategoryErrors
{
    public static Error DuplicatedCategoryName = new Error("Category.DuplicatedName", "Another category with the same name is already exists");
    public static Error CategoryNotFound = new Error("Category.CategoryNotFound", "there was no category with the given id");
}
