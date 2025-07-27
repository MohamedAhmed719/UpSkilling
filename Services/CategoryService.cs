

using Microsoft.AspNetCore.Server.HttpSys;

namespace Task.Api.Services;

public class CategoryService(ApplicationDbContext context) : ICategoryService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<CategoryResponse>> AddAsync(CategoryRequest request,CancellationToken cancellationToken=default!)
    {
        var isCategoryExists = await _context.Categories.AnyAsync(x => x.Name == request.Name,cancellationToken);

        if (isCategoryExists)
            return Result.Failure<CategoryResponse>(CategoryErrors.DuplicatedCategoryName);


        var category = request.Adapt<Category>();

        await _context.AddAsync(category);

        await _context.SaveChangesAsync();


        return Result.Success(category.Adapt<CategoryResponse>());
    }

    public async Task<Result<CategoryResponse>> GetAsync(int id, CancellationToken cancellationToken = default!)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id,cancellationToken);

        if (category is null)
            return Result.Failure<CategoryResponse>(CategoryErrors.CategoryNotFound);


        return Result.Success(category.Adapt<CategoryResponse>());

    }

    public async Task<IEnumerable<CategoryResponse>> GetAllAsync(CancellationToken cancellationToken=default!)
    {

        var categories = await _context.Categories.ProjectToType<CategoryResponse>().ToListAsync(cancellationToken);

        return categories;
    }


    public async Task<Result> UpdateAsync(int id,CategoryRequest request,CancellationToken cancellationToken=default!)
    {
        var isCategoryExists = await _context.Categories.AnyAsync(x => x.Name == request.Name && x.Id != id);

        if(isCategoryExists)
        return Result.Failure(CategoryErrors.DuplicatedCategoryName);

        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (category is null)
            return Result.Failure(CategoryErrors.CategoryNotFound);

        category.Name = request.Name;

        category.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
   
    public async Task<Result> DeleteAsync(int id,CancellationToken cancellationToken)
    {
        var isCategoryExists = await _context.Categories.AnyAsync(x => x.Id == id,cancellationToken);

        if (!isCategoryExists)
            return Result.Failure(CategoryErrors.CategoryNotFound);

        await _context.Categories.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
