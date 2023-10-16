using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace BookStore.Models.ViewModels
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}
