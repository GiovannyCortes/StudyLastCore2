using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudyLastCore2.Models {
    public class ItemsPaginados {
       
        public List<Item> Items { get; set; }

        public int NumRegistros { get; set; }

    }
}