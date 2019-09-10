using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_App.Domain.Models
{
    public class RelationViewModel
    {
        public Guid Id { get; set; }

        public Guid RelationAddressId { get; set; }

        public IList<Guid> Categories { get; set; }

        public bool IsDisabled { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public string TelephoneNumber { get; set; }

        public string Email { get; set; }

        public Guid CountryId { get; set; }

        public string CountryName { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string PostalCode { get; set; }

        public string PostalCodeMask { get; set; }

        public int StreetNumber { get; set; }
    }
}