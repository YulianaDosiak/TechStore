using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TechStore.DAL.Concrete;
using TechStore.DALEF.Concrete.ctx;
using UserDTO = TechStore.DTO.User;
using UserModel = TechStore.DALEF.Models.User;

namespace TechStore.DALEF.Concrete
{
    public class UserDALEF : GenericDAL<UserDTO>
    {
        public UserDALEF(string connStr, IMapper mapper) : base(connStr, mapper) { }

        public override UserDTO Create(UserDTO entity)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var model = _mapper.Map<UserModel>(entity);
                ctx.Users.Add(model);
                ctx.SaveChanges();
                entity.UserID = model.UserId;
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating User: {ex.Message}");
                return null;
            }
        }

        public override bool Delete(int id)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var model = ctx.Users.Find(id);
                if (model == null) return false;
                ctx.Users.Remove(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting User: {ex.Message}");
                return false;
            }
        }

        public override List<UserDTO> GetAll()
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var models = ctx.Users.OrderBy(e => e.UserId).ToList();
                return _mapper.Map<List<UserDTO>>(models);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving Users: {ex.Message}");
                return new List<UserDTO>();
            }
        }

        public override UserDTO GetById(int id)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var model = ctx.Users.Find(id);
                return _mapper.Map<UserDTO>(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving User by Id: {ex.Message}");
                return null;
            }
        }

        public override UserDTO Update(UserDTO entity)
        {
            using var ctx = new TechStoreDbContext(_connStr);
            try
            {
                var existing = ctx.Users.Find(entity.UserID);
                if (existing == null) throw new Exception("User not found");
                _mapper.Map(entity, existing);
                ctx.SaveChanges();
                return _mapper.Map<UserDTO>(existing);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating User: {ex.Message}");
                return null;
            }
        }
    }
}