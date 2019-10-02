namespace MVC_App.Domain.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RelationContext : DbContext
    {
        public RelationContext()
            : base("name=RelationContext")
        {
        }

        public virtual DbSet<AddressType> AddressTypes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Relation> Relations { get; set; }
        public virtual DbSet<RelationAddress> RelationAddresses { get; set; }
        public virtual DbSet<RelationCategory> RelationCategories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressType>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<AddressType>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<AddressType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<AddressType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<AddressType>()
                .Property(e => e.Code1)
                .IsUnicode(false);

            modelBuilder.Entity<AddressType>()
                .Property(e => e.Code2)
                .IsUnicode(false);

            modelBuilder.Entity<AddressType>()
                .Property(e => e.Code3)
                .IsUnicode(false);

            modelBuilder.Entity<AddressType>()
                .Property(e => e.Code4)
                .IsUnicode(false);

            modelBuilder.Entity<AddressType>()
                .Property(e => e.Code5)
                .IsUnicode(false);

            modelBuilder.Entity<AddressType>()
                .Property(e => e.Code6)
                .IsUnicode(false);

            modelBuilder.Entity<AddressType>()
                .HasMany(e => e.RelationAddress)
                .WithRequired(e => e.AddressType)
                .HasForeignKey(e => e.AddressTypeId);

            modelBuilder.Entity<Category>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Code1)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Code2)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Code3)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Code4)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Code5)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.Code6)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.RelationCategory)
                .WithRequired(e => e.tblCategory)
                .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<Country>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.ISO3166_2)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.ISO3166_3)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.PostalCodeFormat)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.RelationAddress)
                .WithOptional(e => e.Country)
                .HasForeignKey(e => e.CountryId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Relation>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.DepartureName)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.ArrivalName)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.DefaultStreet)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.DefaultPostalCode)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.DefaultCity)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.DefaultCountry)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.EMailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.Url)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.IMAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.SkypeAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.TelephoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.MobileNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.FaxNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.EmergencyNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.CustomerCode)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.DebtorNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.VendorNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.InvoiceTo)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.InvoiceEMailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.VatName)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.VatNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.ChamberOfCommerce)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.BankName)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.BankAccount)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.BankBic)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.PriceListName)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.PriceListNameForCollecting)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.InvoiceGroupByTransportOrderColumnName)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.GeneralLedgerAccount)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.CarrierCode)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .Property(e => e.SupplyNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Relation>()
                .HasMany(e => e.RelationAddress)
                .WithRequired(e => e.Relation)
                .HasForeignKey(e => e.RelationId);

            modelBuilder.Entity<Relation>()
                .HasMany(e => e.RelationCategory)
                .WithRequired(e => e.tblRelation)
                .HasForeignKey(e => e.RelationId);

            modelBuilder.Entity<RelationAddress>()
                .Property(e => e.Street)
                .IsUnicode(false);

            modelBuilder.Entity<RelationAddress>()
                .Property(e => e.NumberSuffix)
                .IsUnicode(false);

            modelBuilder.Entity<RelationAddress>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<RelationAddress>()
                .Property(e => e.Province)
                .IsUnicode(false);

            modelBuilder.Entity<RelationAddress>()
                .Property(e => e.Building)
                .IsUnicode(false);

            modelBuilder.Entity<RelationAddress>()
                .Property(e => e.PostalCode)
                .IsUnicode(false);

            modelBuilder.Entity<RelationAddress>()
                .Property(e => e.CountryName)
                .IsUnicode(false);
        }
    }
}
