using Microsoft.EntityFrameworkCore;
using System;
using TechStore.DALEF.Data;
using TechStore.DALEF.Models;

namespace TechStore.DALEF.Concrete.ctx
{
    public class TechStoreDbContext : TechStoreContext
    {
        private readonly string _connStr;

        public TechStoreDbContext(string connStr) : base()
        {
            _connStr = connStr;
        }

        public TechStoreDbContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && !string.IsNullOrEmpty(_connStr))
            {
                optionsBuilder.UseSqlServer(_connStr);
            }
        }
    }
}