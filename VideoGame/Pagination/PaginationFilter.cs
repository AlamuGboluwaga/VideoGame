namespace VideoGame.Pagination
{
    public class PaginationFilter(int pageNumber = 1, int pageSize = 10)
    {
        // Sanitize the inputs immediately using ternary expressions
    public int PageNumber { get; set; } = pageNumber < 1 ? 1 : pageNumber;
    public int PageSize { get; set; } = pageSize > 100 ? 100 : pageSize;
}
}
