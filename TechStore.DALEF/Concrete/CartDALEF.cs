using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TechStore.DAL.Concrete;
using TechStore.DAL.Interfaces;
using TechStore.DALEF.Concrete.ctx;
using CartDTO = TechStore.DTO.Cart;
using CartModel = TechStore.DALEF.Models.Cart;

namespace TechStore.DALEF.Concrete
{
    public class CartDALEF : GenericDAL<CartDTO>, ICartDAL
    {
        public CartDALEF(string connStr, IMapper mapper) : base(connStr, mapper) { }

        public override CartDTO Create(CartDTO entity)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var model = _mapper.Map<CartModel>(entity);
                ctx.Carts.Add(model);
                ctx.SaveChanges();
                entity.CartID = model.CartId;
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
                var model = ctx.Carts.Find(id);
                if (model == null) return false;
                ctx.Carts.Remove(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override List<CartDTO> GetAll()
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var models = ctx.Carts.OrderBy(e => e.CartId).ToList();
                return _mapper.Map<List<CartDTO>>(models);
            }
            catch (Exception)
            {
                return new List<CartDTO>();
            }
        }

        public override CartDTO GetById(int id)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var model = ctx.Carts.Find(id);
                return _mapper.Map<CartDTO>(model);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override CartDTO Update(CartDTO entity)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var existing = ctx.Carts.Find(entity.CartID);
                if (existing == null) throw new Exception("Cart not found");
                _mapper.Map(entity, existing);
                ctx.SaveChanges();
                return _mapper.Map<CartDTO>(existing);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}