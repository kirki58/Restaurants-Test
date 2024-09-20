using System;

namespace Restaurants.API.DTOs.Common;

public class PagedResult<T> where T : class
{
    public PagedResult(IEnumerable<T> items, int totalItems, int pageSize, int pageNo)
    {
        this.PagedItemsList = items;
        this.TotalItems = totalItems;

        this.TotalPages = totalItems / pageSize;
        if(totalItems % pageSize != 0){this.TotalPages++;} // If we got remainders, open a new page for them

        this.ResultFrom = ((pageNo - 1) * pageSize ) + 1;
        this.ResultTo = this.ResultFrom + pageSize -1;
    }
    public IEnumerable<T> PagedItemsList { get; }
    public int TotalItems { get; } // (Unpaged)
    public int TotalPages { get;}
    public int ResultFrom{ get; }
    public int ResultTo { get;}
}
