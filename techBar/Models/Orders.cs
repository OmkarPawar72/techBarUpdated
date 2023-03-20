using System.ComponentModel.DataAnnotations;

namespace techBar.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }

        public int Email { get; set; }

        public int UserId { get; set; }

        public List<OrderItem> OrderItems { get; set; } 
    }
}
