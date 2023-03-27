using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyLastCore2.Models {

    [Table("STORE_ITEMS")]
    public class Item {

        [Key] [Column("IdItem")]
        public int IdItem { get; set; }
        
        [Column("Name")]
        public string Name { get; set; }
        
        [Column("Description")]
        public string Description { get; set; }
        
        [Column("Amount")]
        public int Amount { get; set; }
        
        [Column("Image")]
        public string Image { get; set; }
    }
}
