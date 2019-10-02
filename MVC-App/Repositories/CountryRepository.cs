using MVC_App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_App.Repositories
{
    public class CountryRepository : GenericRepository<Country>
    {
        public CountryRepository(RelationContext context) : base(context)
        {

        }
    }
}