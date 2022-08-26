using MP.ApiDotNet6.Domain.Entities;

namespace MP.ApiDotNet6.Application.DTOs
{
    public class PurchaseDTO
    {
        public string CodErp { get; set; }
        public string Document { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }

    }
}
 