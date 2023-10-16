using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        //public Profile? Profile { get; set; }
        public int RestaurantId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }
    }
}
