namespace MVC_App.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblCountry")]
    public partial class Country
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Country()
        {
            RelationAddress = new HashSet<RelationAddress>();
        }

        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsDefault { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(2)]
        public string ISO3166_2 { get; set; }

        [StringLength(3)]
        public string ISO3166_3 { get; set; }

        public Guid? DefaultVatId { get; set; }

        [StringLength(255)]
        public string PostalCodeFormat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RelationAddress> RelationAddress { get; set; }
    }
}
