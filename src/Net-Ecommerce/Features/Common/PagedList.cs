using Microsoft.EntityFrameworkCore;

namespace Net_Ecommerce.Features.Common;

public class PagedList<T>
{
    private PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        Items = items;
        Count = count;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public List<T> Items { get; }
    public int Count { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public bool HasNextPage => Count > PageNumber * PageSize;
    public bool HasPreviousPage => PageNumber > 1;

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new(items, count, pageNumber, pageSize);
    }
}