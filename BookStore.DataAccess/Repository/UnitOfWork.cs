using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get;private set; }
        public IProductRepository Product { get;private set; }

        public ICartItemRepository CartItem { get; private set; }
        private AppDbContext _db;
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            CartItem = new CartItemRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
