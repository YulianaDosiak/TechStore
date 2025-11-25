using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TechStore.DAL.Concrete;
using TechStore.DAL.Interfaces;
using TechStore.DALEF.Concrete.ctx;
using CartItemsDTO = TechStore.DTO.CartItems;
using CartItemModel = TechStore.DALEF.Models.CartItem;

namespace TechStore.DALEF.Concrete
{
    public class CartItemsDALEF : GenericDAL<CartItemsDTO>, ICartItemsDAL
    {
        public CartItemsDALEF(string connStr, IMapper mapper) : base(connStr, mapper) { }

        public override CartItemsDTO Create(CartItemsDTO entity)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var model = _mapper.Map<CartItemModel>(entity);
                ctx.CartItems.Add(model);
                ctx.SaveChanges();
                entity.CartItemID = model.CartItemId;
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
                var model = ctx.CartItems.Find(id);
                if (model == null) return false;
                ctx.CartItems.Remove(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override List<CartItemsDTO> GetAll()
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var models = ctx.CartItems.OrderBy(e => e.CartItemId).ToList();
                return _mapper.Map<List<CartItemsDTO>>(models);
            }
            catch (Exception)
            {
                return new List<CartItemsDTO>();
            }
        }

        public override CartItemsDTO GetById(int id)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var model = ctx.CartItems.Find(id);
                return _mapper.Map<CartItemsDTO>(model);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override CartItemsDTO Update(CartItemsDTO entity)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var existing = ctx.CartItems.Find(entity.CartItemID);
                if (existing == null) throw new Exception("CartItem not found");
                _mapper.Map(entity, existing);
                ctx.SaveChanges();
                return _mapper.Map<CartItemsDTO>(existing);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}