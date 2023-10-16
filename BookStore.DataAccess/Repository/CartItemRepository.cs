using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.RepositoryInterface;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        private AppDbContext _db;
        public CartItemRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CartItem cartItem)
        {
            _db.CartItems.Update(cartItem);
        }
    }
}
