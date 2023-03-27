using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace StudyLastCore2.Models {
    [Table("STORE_ORDER_ITEMS")]
    public class OrderItems {
        
        [Key] [Column("IdOrder")]
        public int IdOrder { get; set; }
        
        [Column("IdItem")]
        public int IdItem { get; set; }
        
        [Column("Ammount")]
        public int Amount { get; set; }
    }
}
