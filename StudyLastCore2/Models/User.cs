using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyLastCore2.Models {
    [Table("STORE_USERS")]
    public class User {

        [Key] [Column("IdUser")]
        public int IdUser { get; set; }
        
        [Column("Name")]
        public string Name { get; set; }
        
        [Column("Password")]
        public byte[] Password { get; set; }
        
        [Column("Salt")]
        public string Salt { get; set; }
        
        [Column("Role")]
        public string Role { get; set; }

    }
}
