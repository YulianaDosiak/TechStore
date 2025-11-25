using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TechStore.DAL.Concrete;
using TechStore.DAL.Interfaces;
using TechStore.DALEF.Concrete.ctx;
using OrderItemDTO = TechStore.DTO.OrderItem;
using OrderItemModel = TechStore.DALEF.Models.OrderItem;

namespace TechStore.DALEF.Concrete
{
    public class OrderItemDALEF : GenericDAL<OrderItemDTO>, IOrderItemDAL
    {
        public OrderItemDALEF(string connStr, IMapper mapper) : base(connStr, mapper) { }

        public override OrderItemDTO Create(OrderItemDTO entity)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var model = _mapper.Map<OrderItemModel>(entity);
                ctx.OrderItems.Add(model);
                ctx.SaveChanges();
                entity.OrderItemID = model.OrderItemId;
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
                var model = ctx.OrderItems.Find(id);
                if (model == null) return false;
                ctx.OrderItems.Remove(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override List<OrderItemDTO> GetAll()
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var models = ctx.OrderItems.OrderBy(e => e.OrderItemId).ToList();
                return _mapper.Map<List<OrderItemDTO>>(models);
            }
            catch (Exception)
            {
                return new List<OrderItemDTO>();
            }
        }

        public override OrderItemDTO GetById(int id)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var model = ctx.OrderItems.Find(id);
                return _mapper.Map<OrderItemDTO>(model);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override OrderItemDTO Update(OrderItemDTO entity)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var existing = ctx.OrderItems.Find(entity.OrderItemID);
                if (existing == null) throw new Exception("OrderItem not found");
                _mapper.Map(entity, existing);
                ctx.SaveChanges();
                return _mapper.Map<OrderItemDTO>(existing);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}