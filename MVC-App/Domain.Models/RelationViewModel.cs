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

        [Required(AllowEmptyStrings = false)]
        [StringLength(128, MinimumLength = 2, ErrorMessage = "Input should be between 2 and 128 characters")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(128, MinimumLength = 2, ErrorMessage = "Input should be between 2 and 128 characters")]
        public string FullName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        public string TelephoneNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public Guid CountryId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(128, MinimumLength = 2, ErrorMessage = "Input should be between 2 and 128 characters")]
        public string CountryName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(128, ErrorMessage = "Input should be less than 128 characters")]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(128, ErrorMessage = "Input should be less than 128 characters")]
        public string Street { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.PostalCode, ErrorMessage = "Invalid Postal Code")]
        public string PostalCode { get; set; }

        public string PostalCodeMask { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Street Number")]
        public int StreetNumber { get; set; }
    }
}