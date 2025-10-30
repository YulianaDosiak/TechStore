using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TechStore.DAL.Concrete;
using TechStore.DALEF.Concrete.ctx;
using CartItemsDTO = TechStore.DTO.CartItems;
using CartItemsModel = TechStore.DALEF.Models.CartItem;

namespace TechStore.DALEF.Concrete
{
    public class CartItemsDALEF : GenericDAL<CartItemsDTO>
    {
        public CartItemsDALEF(string connStr, IMapper mapper) : base(connStr, mapper) { }

        public override CartItemsDTO Create(CartItemsDTO entity)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var model = _mapper.Map<CartItemsModel>(entity);
                ctx.CartItems.Add(model);
                ctx.SaveChanges();
                entity.CartItemID = model.CartItemId;
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating CartItem: {ex.Message}");
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting CartItem: {ex.Message}");
                return false;
            }
        }

        public override List<CartItemsDTO> GetAll()
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var models = ctx.CartItems
                    .Include(ci => ci.Cart)
                    .Include(ci => ci.Product)
                    .OrderBy(ci => ci.CartItemId)
                    .ToList();
                return _mapper.Map<List<CartItemsDTO>>(models);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving CartItems: {ex.Message}");
                return new List<CartItemsDTO>();
            }
        }

        public override CartItemsDTO GetById(int id)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var model = ctx.CartItems
                    .Include(ci => ci.Cart)
                    .Include(ci => ci.Product)
                    .FirstOrDefault(ci => ci.CartItemId == id);
                return _mapper.Map<CartItemsDTO>(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving CartItem by Id: {ex.Message}");
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating CartItem: {ex.Message}");
                return null;
            }
        }
    }
}