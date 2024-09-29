namespace WebApplication1.Response
{
    public class PaginatedApiResponse<T>
    {
        public T? Data { get; set; }
        public int? TotalItemDataBase { get; set; }

        public int? TotalItemInList { get; set; }
    }
}
