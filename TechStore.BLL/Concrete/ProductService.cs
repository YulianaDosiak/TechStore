using System;
using System.Collections.Generic;
using TechStore.BLL.Interfaces;
using TechStore.DAL.Interfaces;
using TechStore.DTO;

namespace TechStore.BLL.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductDAL _productDal;

        public ProductService(IProductDAL productDal)
        {
            _productDal = productDal;
        }

        public List<Product> GetAllProducts()
        {
            return _productDal.GetAll();
        }

        public Product GetProductById(int id)
        {
            return _productDal.GetById(id);
        }

        public void AddProduct(Product product)
        {
            if (product.Price < 0) throw new ArgumentException("Price cannot be negative");
            _productDal.Create(product);
        }

        public void UpdateProduct(Product product)
        {
            if (product.Price < 0) throw new ArgumentException("Price cannot be negative");
            _productDal.Update(product);
        }

        public void DeleteProduct(int id)
        {
            _productDal.Delete(id);
        }
    }
}