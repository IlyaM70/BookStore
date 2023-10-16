using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        // for anonymous shopping
        public string CartId { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
