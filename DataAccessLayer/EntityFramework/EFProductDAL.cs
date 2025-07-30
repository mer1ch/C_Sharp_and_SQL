using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;

namespace DataAccessLayer.EntityFramework
{
    public class EFProductDAL : GenericRepository<Product>, IProductDAL
    {
        public List<Object> GetProductsWithCategory()
        {
            var context = new CampContext();
            var values = context.Products.Select(p => new
            {
               ProductId = p.ProductId, 
               ProductName = p.ProductName,
               ProductDescription = p.ProductDescription,
               ProductPrice = p.ProductPrice,
               ProductStock = p.ProductStock,
               CategoryName = p.Category.CategoryName
            }).ToList();
            return values.Cast<object>().ToList();
        }
    }
}
