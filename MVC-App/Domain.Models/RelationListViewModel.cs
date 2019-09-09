using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_App.Domain.Models
{
    public class RelationListViewModel
    {
        public IEnumerable<RelationViewModel> RelationViewModels { get; set; }

        public SelectList Categories { get; set; }
    }
}