using MVC_App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVC_App.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private RelationContext context = new RelationContext();

        private GenericRepository<Relation> relationRepository;

        private GenericRepository<RelationAddress> relationAddressRepository;

        private GenericRepository<Country> countryRepository;

        private GenericRepository<Category> categoryRepository;

        private GenericRepository<RelationCategory> relationCategoryRepository;

        public GenericRepository<Relation> RelationRepository
        {
            get
            {
                return this.relationRepository ?? new GenericRepository<Relation>(context);
            }
            set { relationRepository = value; }
        }

        public GenericRepository<RelationAddress> RelationAddressRepository
        {
            get
            {
                return this.relationAddressRepository ?? new GenericRepository<RelationAddress>(context);
            }
            set { relationAddressRepository = value; }
        }

        public GenericRepository<Country> CountryRepository
        {
            get
            {
                return this.countryRepository ?? new GenericRepository<Country>(context);
            }
            set { countryRepository = value; }
        }

        public GenericRepository<Category> CategoryRepository
        {
            get
            {
                return this.categoryRepository ?? new GenericRepository<Category>(context);
            }
            set { categoryRepository = value; }
        }

        public GenericRepository<RelationCategory> RelationCategoryRepository
        {
            get
            {
                return this.relationCategoryRepository ?? new GenericRepository<RelationCategory>(context);
            }
            set { relationCategoryRepository = value; }
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}