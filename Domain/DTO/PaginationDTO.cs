namespace Autoglass.Domain.DTO
{
    public class PaginationDTO
    {
        public PaginationDTO(int page, int limit)
        {
            Page = page;
            Limit = limit;
        }

        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
