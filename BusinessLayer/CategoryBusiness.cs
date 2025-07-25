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
    public class CategoryBusiness
    {
        private readonly CategoryRepository _repo = new();

        public List<CategoryDTO> GetAll() =>
            _repo.GetAll().Select(c => new CategoryDTO
            {
                CategoryID = c.CategoryID,
                Name = c.Name,
                Picture = c.Picture,
                Description = c.Description
            }).ToList();

        public void Add(CategoryDTO dto) =>
            _repo.Add(new Category
            {
                Name = dto.Name,
                Picture = dto.Picture,
                Description = dto.Description
            });

        public void Update(CategoryDTO dto) =>
            _repo.Update(new Category
            {
                CategoryID = dto.CategoryID,
                Name = dto.Name,
                Picture = dto.Picture,
                Description = dto.Description
            });

        public void Delete(int id) => _repo.Delete(id);
    }

}
