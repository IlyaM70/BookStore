using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.RepositoryInterface;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private AppDbContext _db;
        public ProductRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            //explicit updating
            var productFromDb = _db.Products.FirstOrDefault(p => p.Id == product.Id);
            if (productFromDb != null)
            {
                productFromDb.Title = product.Title;
                productFromDb.ISBN = product.ISBN;
                productFromDb.Price = product.Price;
                productFromDb.Description = product.Description;
                productFromDb.Author = product.Author;
                productFromDb.CategoryId = product.CategoryId;
                if (product.ImageUrl!=null)
                {
                    productFromDb.ImageUrl = product.ImageUrl;
                }
                _db.Products.Update(productFromDb);
                _db.SaveChanges();
            }
        }
    }
}
