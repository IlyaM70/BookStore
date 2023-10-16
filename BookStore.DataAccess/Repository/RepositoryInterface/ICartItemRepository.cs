using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository.RepositoryInterface
{
    public interface ICartItemRepository: IRepository<CartItem>
    {
        void Update(CartItem cartItem);
    }
}
