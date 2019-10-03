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

        public string SortOrderByName { get; set; }

        public string SortOrderByFullName { get; set; }

        public string SortOrderByTelephoneNumber { get; set; }

        public string SortOrderByEmail { get; set; }

        public string SortOrderByCountry { get; set; }

        public string SortOrderByCity { get; set; }

        public string SortOrderByStreet { get; set; }

        public string SortOrderByPostalCode { get; set; }
    }
}