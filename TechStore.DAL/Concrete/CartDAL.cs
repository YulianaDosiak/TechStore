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
    public class CartDAL : GenericDAL<Cart>, ICartDAL
    {
        public CartDAL(string connStr, IMapper mapper) : base(connStr, mapper) { }

        public override Cart Create(Cart entity)
        {
            throw new NotImplementedException();
        }

        public override List<Cart> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Cart GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override Cart Update(Cart entity)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}