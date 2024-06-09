using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingList.Models
{
    public class Item
    {
        [Key]
        public int Itemid { get; set; }

        [Display(Name = "Item name")]
        [Required(ErrorMessage = "Item name is required")]
        public string Name { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity is required")]
        public string Quantity { get; set; }
        [Display(Name = "Done")]
     
        public bool Bought { get; set; }

        public int UserId { get; set; }
        public UserAccount UserAccount { get; set; }

    }
}
