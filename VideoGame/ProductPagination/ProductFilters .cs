//namespace VideoGame.ProductPagination
//{
//    public class ProductFilters(int pageNumber =1 , int pageSize =10)
//    {
//        public int PageNumber { get; set; } = pageNumber < 1 ? 1 :pageNumber ;
//        public int PageSize { get; set; } = pageSize > 100 ? 100 : pageSize;
//    }
//}
//

namespace VideoGame.ProductPagination
{
    public class ProductFilters
    {
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize{get => _pageSize; set => _pageSize = value > 100 ? 100 : value;}
    }
}