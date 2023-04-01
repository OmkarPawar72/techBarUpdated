using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace techBar.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }

        public string Email { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public List<OrderItem> OrderItems { get; set; } 
    }
}
