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
    public class UserDAL : GenericDAL<User>, IUserDAL
    {
        public UserDAL(string connStr, IMapper mapper) : base(connStr, mapper) { }

        public override User Create(User entity)
        {

            throw new NotImplementedException();
        }

        public override List<User> GetAll()
        {

            throw new NotImplementedException();
        }

        public override User GetById(int id)
        {

            throw new NotImplementedException();
        }

        public override User Update(User entity)
        {

            throw new NotImplementedException();
        }

        public override bool Delete(int id)
        {

            throw new NotImplementedException();
        }
    }
}