namespace MakersMarket.Data.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Repositories;
    using Models;
    public class MakersMarketData:IMakersMarketData
    {
        public MakersMarketDbContext context;
        private IDictionary<Type, object> repositories;

        public MakersMarketData(MakersMarketDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }
        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }
        
        public IRepository<Category> Categories
        {
            get { return this.GetRepository<Category>(); }
        }

        public IRepository<Brand> Brands
        {
            get { return this.GetRepository<Brand>(); }
        }

        public IRepository<CartItem> CartItems
        {
            get { return this.GetRepository<CartItem>(); }
        }

        public IRepository<Image> Images
        {
            get { return this.GetRepository<Image>(); }
        }

        public IRepository<Product> Products
        {
            get { return this.GetRepository<Product>(); }
        }

        public IRepository<Shop> Shops
        {
            get { return this.GetRepository<Shop>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
        public IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof (T);

            if (!this.repositories.ContainsKey(type))
            {
                var typeOfRepository = typeof(GenericRepository<T>);

                var repository = Activator.CreateInstance(typeOfRepository, this.context);

                this.repositories.Add(type, repository);
            }

            return (IRepository<T>)this.repositories[type];
        }
    }
}
