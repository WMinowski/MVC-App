//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_App.Domain.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblCountry
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCountry()
        {
            this.tblRelationAddress = new HashSet<tblRelationAddress>();
        }
    
        public System.Guid Id { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsDefault { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ISO3166_2 { get; set; }
        public string ISO3166_3 { get; set; }
        public Nullable<System.Guid> DefaultVatId { get; set; }
        public string PostalCodeFormat { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblRelationAddress> tblRelationAddress { get; set; }
    }
}
