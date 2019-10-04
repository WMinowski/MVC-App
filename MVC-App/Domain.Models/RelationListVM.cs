using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_App.Domain.Models
{
    public class RelationListVM
    {
        public IEnumerable<RelationVM> RelationViewModels { get; set; }

        public SelectList Categories { get; set; }

        public List<Country> Countries { get; set; }

        public string OrderBy { get; set; }
    }
}