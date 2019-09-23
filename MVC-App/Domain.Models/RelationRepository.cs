using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC_App.Domain.Models
{
    public class RelationRepository : IRelationRepository, IDisposable
    {
        public RelationContext DbContext { get; set; }

        public RelationRepository()
        {
            DbContext = new RelationContext();
        }

        public void Dispose()
        {
            ((IDisposable)DbContext).Dispose();
        }
    }
}