using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.DAL.Interfaces;
using TechStore.DTO;

namespace TechStore.DAL.Concrete
{
    public class CartItemsDAL : GenericDAL<CartItems>, ICartItemsDAL
    {
        public CartItemsDAL(string connStr, IMapper mapper) : base(connStr, mapper) { }

        public override CartItems Create(CartItems entity)
        {
            throw new NotImplementedException();
        }

        public override List<CartItems> GetAll()
        {
            throw new NotImplementedException();
        }

        public override CartItems GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override CartItems Update(CartItems entity)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}