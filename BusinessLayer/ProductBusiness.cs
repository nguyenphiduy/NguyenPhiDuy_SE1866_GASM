using BusinessLayer.DTO;
using RepositoryLayer;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ProductBusiness
    {
        private readonly ProductRepository _repo = new();

        public List<ProductDTO> GetAll()
        {
            return _repo.GetAll().Select(p => new ProductDTO
            {
                ProductID = p.ProductID,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                CategoryID = p.CategoryID,
                CategoryName = p.Category?.Name,
                OrderID = p.OrderID,
                CategoryPicture = p.Category?.Picture,
                CategoryDescription = p.Category?.Description

            }).ToList();
        }

        public void Add(ProductDTO dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                CategoryID = dto.CategoryID,
                OrderID = dto.OrderID
            };
            _repo.Add(product);
        }

        public void Update(ProductDTO dto)
        {
            var product = new Product
            {
                ProductID = dto.ProductID,
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                CategoryID = dto.CategoryID,
                OrderID = dto.OrderID
            };
            _repo.Update(product);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }

}
