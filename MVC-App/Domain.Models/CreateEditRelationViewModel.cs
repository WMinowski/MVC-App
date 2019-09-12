using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_App.Domain.Models
{
    public class CreateEditRelationViewModel
    {
        public RelationViewModel Relation { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }
    }
}