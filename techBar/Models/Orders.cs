using System.ComponentModel.DataAnnotations;

namespace techBar.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }

        public string Email { get; set; }

        public string UserId { get; set; }

        public List<OrderItem> OrderItems { get; set; } 
    }
}
