using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlutterBackEnd.Models
{
    [Table("GroceryItem")]
    public class GroceryModel
    {
        [Key]
        public int Id { get; set; }
        public String? name { get; set; }
        public bool? isBought { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
