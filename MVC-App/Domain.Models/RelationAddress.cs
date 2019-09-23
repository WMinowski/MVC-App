namespace MVC_App.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblRelationAddress")]
    public partial class RelationAddress
    {
        public Guid RelationId { get; set; }

        public Guid AddressTypeId { get; set; }

        [StringLength(255)]
        public string Street { get; set; }

        public int? Number { get; set; }

        [StringLength(50)]
        public string NumberSuffix { get; set; }

        [StringLength(255)]
        public string City { get; set; }

        [StringLength(255)]
        public string Province { get; set; }

        [StringLength(255)]
        public string Building { get; set; }

        [StringLength(50)]
        public string PostalCode { get; set; }

        public Guid? CountryId { get; set; }

        [StringLength(50)]
        public string CountryName { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        public Guid Id { get; set; }

        public virtual AddressType tblAddressType { get; set; }

        public virtual Country tblCountry { get; set; }

        public virtual Relation tblRelation { get; set; }
    }
}
