using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            _products = new List<Product> {
            new Product{ProductId=1,CategoryId=1,ProductName="Bardak",UnitPrice=15,UnitsInStock=15 },
            new Product{ProductId=2,CategoryId=1,ProductName="Kamera",UnitPrice=500,UnitsInStock=3 },
            new Product{ProductId=3,CategoryId=2,ProductName="Telefon",UnitPrice=4500,UnitsInStock=2 },
            new Product{ProductId=4,CategoryId=2,ProductName="Klavye",UnitPrice=100,UnitsInStock=20 },
            new Product{ProductId=5,CategoryId=2,ProductName="Mouse",UnitPrice=50,UnitsInStock=15 }
            
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            // foreach yerine kullanılan dizide eleman bulmak için kullanılan metot
            Product productToDelete = _products.SingleOrDefault(p=>p.ProductId==product.ProductId);
            _products.Remove(productToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            //sql deki gibi koşula sağlayanları yeni bir tablo yapar
           return  _products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public void Update(Product product)
        {
            // gönderilen ürün id'sine sahip olan listedeki ürünü bulan kod.
            // Eşitliğin sağı update edilen solu ise veri tabanında güncellenen 
            // veri tabanında o ürüne ise tam alttaki kod ile eriştik
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);

            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }
    }
}
