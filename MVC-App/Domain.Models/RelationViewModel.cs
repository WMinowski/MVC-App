using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        [StringLength(128)]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        public string TelephoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public Guid CountryId { get; set; }

        public string CountryName { get; set; }

        [Required]
        [StringLength(128)]
        public string City { get; set; }

        [Required]
        [StringLength(128)]
        public string Street { get; set; }

        [Required]
        [DataType(DataType.PostalCode, ErrorMessage = "Invalid Postal Code")]
        public string PostalCode { get; set; }

        public string PostalCodeMask { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Street Number")]
        public int StreetNumber { get; set; }
    }
}