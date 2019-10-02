using MVC_App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_App.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(RelationContext context) : base(context)
        {

        }
    }
}