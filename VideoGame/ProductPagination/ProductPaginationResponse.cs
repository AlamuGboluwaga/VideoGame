namespace VideoGame.ProductPagination
{
    public class ProductPaginationResponse
    {
        //p2t2
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecord { get; set; }
        public int TotalPages { get; set; }

    }
}
