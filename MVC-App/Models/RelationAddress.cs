using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_App.Models
{
    public class RelationAddress
    {
        public int RelationId { get; set; }

        public Relation Relation { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string PostalCode { get; set; }

        public int Number { get; set; }
    }
}