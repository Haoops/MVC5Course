using System;
using System.Linq;
using System.Collections.Generic;

namespace MVC5Course.Models
{
    public class ProductRepository : EFRepository<Product>, IProductRepository
    {
        public override IQueryable<Product> All()
        {
            return base.All().Where(p => !p.IsDelete);
        }

        public IQueryable<Product> All(bool isAll,bool delete)
        {
            if (isAll)
            {
                if (delete)
                {
                    return base.All().Where(p => p.IsDelete);

                }
                else {

                    return base.All().Where(p => !p.IsDelete);
                }
            }
            else
            {
                if (delete)
                {
                    return this.All().Where(p => p.IsDelete);

                }
                else {

                    return this.All().Where(p => !p.IsDelete);
                }
            }
        }

        public Product Find(int id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }

    }

    public interface IProductRepository : IRepository<Product>
    {

    }
}