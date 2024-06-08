namespace Autoglass.Domain.DTO
{
    public class QueryProdutoDTO
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string Description { get; set; }
    }
}
