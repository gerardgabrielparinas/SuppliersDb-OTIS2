using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SuppliersDb.Models
{
    public class Item
    {
        [Key]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Required.")]
        public string Representative { get; set; }

        public string Code { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Date Modified")]
        public DateTime? DateModified { get; set; }

        [Display(Name = "Item Type")]
        public ItemType Type { get; set; }
    }

    public enum ItemType
    {
        Local = 1,
        International = 2
    }

}
