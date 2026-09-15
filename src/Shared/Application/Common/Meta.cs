namespace Mcm.Shared.Application.Common
{
    public class Meta
    {
        public long Total { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public int PageCount => (int) Math.Ceiling(Total / (decimal) Limit);
        public bool HasNextPage => PageCount > Page;
        public bool HasPreviousPage => Page > 1;
    }
}