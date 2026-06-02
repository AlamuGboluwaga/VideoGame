namespace VideoGame.ProductPagination
{
    public class ProductPaginationResponse(int pageNumber, int pageSize, int totalRecord)
    {
        //p2t2
        public int PageNumber { get; set; } =pageNumber;
        public int PageSize { get; set; } = pageSize;
        public int TotalRecord { get; set; } =  totalRecord;
        public int TotalPages { get; set; } = (int)Math.Ceiling((double)totalRecord / pageSize);

    }
}
