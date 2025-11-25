using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TechStore.DAL.Concrete;
using TechStore.DAL.Interfaces;
using TechStore.DALEF.Concrete.ctx;
using CategoryDTO = TechStore.DTO.Category;
using CategoryModel = TechStore.DALEF.Models.Category;

namespace TechStore.DALEF.Concrete
{
    public class CategoryDALEF : GenericDAL<CategoryDTO>, ICategoryDAL
    {
        public CategoryDALEF(string connStr, IMapper mapper) : base(connStr, mapper) { }

        public override CategoryDTO Create(CategoryDTO entity)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var model = _mapper.Map<CategoryModel>(entity);
                ctx.Categories.Add(model);
                ctx.SaveChanges();
                entity.CategoryID = model.CategoryId;
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override bool Delete(int id)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var model = ctx.Categories.Find(id);
                if (model == null) return false;
                ctx.Categories.Remove(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override List<CategoryDTO> GetAll()
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var models = ctx.Categories.OrderBy(e => e.CategoryId).ToList();
                return _mapper.Map<List<CategoryDTO>>(models);
            }
            catch (Exception)
            {
                return new List<CategoryDTO>();
            }
        }

        public override CategoryDTO GetById(int id)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var model = ctx.Categories.Find(id);
                return _mapper.Map<CategoryDTO>(model);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override CategoryDTO Update(CategoryDTO entity)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var existing = ctx.Categories.Find(entity.CategoryID);
                if (existing == null) throw new Exception("Category not found");
                _mapper.Map(entity, existing);
                ctx.SaveChanges();
                return _mapper.Map<CategoryDTO>(existing);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}