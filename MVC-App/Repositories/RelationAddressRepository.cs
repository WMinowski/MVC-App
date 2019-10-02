using MVC_App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_App.Repositories
{
    public class RelationAddressRepository : GenericRepository<RelationAddress>
    {
        public RelationAddressRepository(RelationContext context) : base(context)
        {

        }
    }
}