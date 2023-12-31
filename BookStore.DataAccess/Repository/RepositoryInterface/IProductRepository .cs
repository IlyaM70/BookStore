﻿using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository.RepositoryInterface
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}
