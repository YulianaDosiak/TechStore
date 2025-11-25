using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TechStore.DAL.Concrete;
using TechStore.DAL.Interfaces;
using TechStore.DALEF.Concrete.ctx;
using OrderDTO = TechStore.DTO.Order;
using OrderModel = TechStore.DALEF.Models.Order;

namespace TechStore.DALEF.Concrete
{
    public class OrderDALEF : GenericDAL<OrderDTO>, IOrderDAL
    {
        public OrderDALEF(string connStr, IMapper mapper) : base(connStr, mapper) { }

        public override OrderDTO Create(OrderDTO entity)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var model = _mapper.Map<OrderModel>(entity);
                ctx.Orders.Add(model);
                ctx.SaveChanges();
                entity.OrderID = model.OrderId;
                return entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public override bool Delete(int id)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var model = ctx.Orders.Find(id);
                if (model == null) return false;
                ctx.Orders.Remove(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override List<OrderDTO> GetAll()
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var models = ctx.Orders.OrderBy(e => e.OrderId).ToList();
                return _mapper.Map<List<OrderDTO>>(models);
            }
            catch (Exception)
            {
                return new List<OrderDTO>();
            }
        }

        public override OrderDTO GetById(int id)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var model = ctx.Orders.Find(id);
                return _mapper.Map<OrderDTO>(model);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override OrderDTO Update(OrderDTO entity)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var existing = ctx.Orders.Find(entity.OrderID);
                if (existing == null) throw new Exception("Order not found");
                _mapper.Map(entity, existing);
                ctx.SaveChanges();
                return _mapper.Map<OrderDTO>(existing);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}