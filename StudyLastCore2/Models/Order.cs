using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace StudyLastCore2.Models {

    [Table("STORE_ORDERS")]
    public class Order {
        
        [Key] [Column("IdOrder")]
        public int IdOrder { get; set; }
        
        [Column("IdUser")]
        public int IdUser { get; set; }
        
        [Column("DateOrder")]
        public DateTime DateOrder { get; set; }

    }
}
