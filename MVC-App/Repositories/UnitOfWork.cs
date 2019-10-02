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
        private readonly RelationContext context = new RelationContext();

        private RelationRepository relationRepository;

        private RelationAddressRepository relationAddressRepository;

        private CountryRepository countryRepository;

        private CategoryRepository categoryRepository;

        private RelationCategoryRepository relationCategoryRepository;

        public RelationRepository RelationRepository
        {
            get
            {
                return this.relationRepository ?? new RelationRepository(context);
            }
            set { relationRepository = value; }
        }

        public RelationAddressRepository RelationAddressRepository
        {
            get
            {
                return this.relationAddressRepository ?? new RelationAddressRepository(context);
            }
            set { relationAddressRepository = value; }
        }

        public CountryRepository CountryRepository
        {
            get
            {
                return this.countryRepository ?? new CountryRepository(context);
            }
            set { countryRepository = value; }
        }

        public CategoryRepository CategoryRepository
        {
            get
            {
                return this.categoryRepository ?? new CategoryRepository(context);
            }
            set { categoryRepository = value; }
        }

        public RelationCategoryRepository RelationCategoryRepository
        {
            get
            {
                return this.relationCategoryRepository ?? new RelationCategoryRepository(context);
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