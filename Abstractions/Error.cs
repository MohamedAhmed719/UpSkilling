namespace Task.Api.Abstractions;

public record Error(string Code,string Description)
{
    public static Error None = new Error(string.Empty, string.Empty);
}
