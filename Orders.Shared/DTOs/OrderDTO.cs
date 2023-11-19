using PGCELL.Shared.Enums;

namespace PGCELL.Shared.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public string Remarks { get; set; } = string.Empty;
    }
}