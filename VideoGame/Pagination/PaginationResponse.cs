namespace VideoGame.Pagination
{
    public class PaginationResponse(int pageNumber, int pageSize, int totalRecords)
    {
        //PPTT
        public int PageNumber { get; set; } = pageNumber;
        public int PageSize { get; set; } = pageSize;
        public int TotalRecords { get; set; } = totalRecords;
      
        // Calculated property based on constructor inputs
        public int TotalPages { get; init; } = (int)Math.Ceiling((double)totalRecords / pageSize);

    }
}
