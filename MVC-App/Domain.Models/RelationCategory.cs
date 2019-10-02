namespace MVC_App.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblRelationCategory")]
    public partial class RelationCategory
    {
        public Guid RelationId { get; set; }

        public Guid CategoryId { get; set; }

        public Guid Id { get; set; }

        public virtual Category tblCategory { get; set; }

        public virtual Relation tblRelation { get; set; }
    }
}
