namespace MVC_App.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblRelation")]
    public partial class Relation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Relation()
        {
            RelationAddress = new HashSet<RelationAddress>();
            RelationCategory = new HashSet<RelationCategory>();
        }

        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; } = "admin";

        public DateTime? ModifiedAt { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool IsDisabled { get; set; }

        public Guid? ParentRelationId { get; set; }

        public bool IsTemporary { get; set; }

        public bool IsMe { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string FullName { get; set; }

        [StringLength(255)]
        public string DepartureName { get; set; }

        [StringLength(255)]
        public string ArrivalName { get; set; }

        [StringLength(255)]
        public string DefaultStreet { get; set; }

        [StringLength(50)]
        public string DefaultPostalCode { get; set; }

        [StringLength(50)]
        public string DefaultCity { get; set; }

        [StringLength(50)]
        public string DefaultCountry { get; set; }

        [StringLength(255)]
        public string EMailAddress { get; set; }

        [StringLength(255)]
        public string Url { get; set; }

        [StringLength(255)]
        public string IMAddress { get; set; }

        [StringLength(255)]
        public string SkypeAddress { get; set; }

        [StringLength(255)]
        public string TelephoneNumber { get; set; }

        [StringLength(255)]
        public string MobileNumber { get; set; }

        [StringLength(255)]
        public string FaxNumber { get; set; }

        [StringLength(255)]
        public string EmergencyNumber { get; set; }

        public DateTime? DepartureBetween { get; set; }

        public DateTime? DepartureBetweenAnd { get; set; }

        public DateTime? ArrivalBetween { get; set; }

        public DateTime? ArrivalBetweenAnd { get; set; }

        public string Remarks { get; set; }

        [StringLength(255)]
        public string CustomerCode { get; set; }

        [StringLength(255)]
        public string DebtorNumber { get; set; }

        [StringLength(255)]
        public string VendorNumber { get; set; }

        [StringLength(255)]
        public string InvoiceTo { get; set; }

        [StringLength(255)]
        public string InvoiceEMailAddress { get; set; }

        public bool? SendInvoiceDigital { get; set; }

        public Guid? VatId { get; set; }

        [StringLength(50)]
        public string VatName { get; set; }

        public double? PaymentTerm { get; set; }

        public bool PaymentViaAutomaticDebit { get; set; }

        [StringLength(255)]
        public string VatNumber { get; set; }

        [StringLength(255)]
        public string ChamberOfCommerce { get; set; }

        [StringLength(255)]
        public string BankName { get; set; }

        [StringLength(255)]
        public string BankAccount { get; set; }

        [StringLength(255)]
        public string BankBic { get; set; }

        public double? CalculateMinimalPrice { get; set; }

        public bool? CalculatePriceManually { get; set; }

        public bool? CalculatePriceByPriceList { get; set; }

        public Guid? PriceListId { get; set; }

        [StringLength(50)]
        public string PriceListName { get; set; }

        public bool? CalculatePriceBasedOnPositions { get; set; }

        public bool? CalculatePriceBasedOnAmount { get; set; }

        public bool? CalculatePriceBasedOnWeight { get; set; }

        public bool? CalculatePriceBasedOnDistance { get; set; }

        public bool? CalculatePriceBasedOnTonne { get; set; }

        public bool? CalculatePriceByFixed { get; set; }

        public bool? CalculatePriceByDistance { get; set; }

        public bool? CalculatePriceBasedOnEpq { get; set; }

        public bool? CalculatePriceBasedOnLoadingMeters { get; set; }

        public bool? CalculatePriceBasedOnVolume { get; set; }

        public double? CalculatePriceByFixedPrice { get; set; }

        public double? CalculateMinimalPriceForCollecting { get; set; }

        public bool? CalculatePriceManuallyForCollecting { get; set; }

        public bool? CalculatePriceByPriceListForCollecting { get; set; }

        public Guid? PriceListIdForCollecting { get; set; }

        [StringLength(50)]
        public string PriceListNameForCollecting { get; set; }

        public bool? CalculatePriceBasedOnPositionsForCollecting { get; set; }

        public bool? CalculatePriceBasedOnAmountForCollecting { get; set; }

        public bool? CalculatePriceBasedOnWeightForCollecting { get; set; }

        public bool? CalculatePriceBasedOnDistanceForCollecting { get; set; }

        public bool? CalculatePriceBasedOnTonneForCollecting { get; set; }

        public bool? CalculatePriceByFixedForCollecting { get; set; }

        public bool? CalculatePriceByDistanceForCollecting { get; set; }

        public bool? CalculatePriceBasedOnEpqForCollecting { get; set; }

        public bool? CalculatePriceBasedOnLoadingMetersForCollecting { get; set; }

        public bool? CalculatePriceBasedOnVolumeForCollecting { get; set; }

        public double? CalculatePriceByFixedPriceForCollecting { get; set; }

        public string GeographicalRegions { get; set; }

        public bool? SendDigitalFreightDocumentsByEMail { get; set; }

        public Guid? DigitalFreightDocumentEMailTemplateId { get; set; }

        public bool? SendFreightStatusUpdateByEMail { get; set; }

        public bool? DepartureTimeSlotsAreAllEqual { get; set; }

        public Guid? DepartureTimeSlotIdOnSundays { get; set; }

        public Guid? DepartureTimeSlotIdOnMondays { get; set; }

        public Guid? DepartureTimeSlotIdOnTuesdays { get; set; }

        public Guid? DepartureTimeSlotIdOnWednesdays { get; set; }

        public Guid? DepartureTimeSlotIdOnThursdays { get; set; }

        public Guid? DepartureTimeSlotIdOnFridays { get; set; }

        public Guid? DepartureTimeSlotIdOnSaturdays { get; set; }

        public bool? ArrivalTimeSlotsAreAllEqual { get; set; }

        public Guid? ArrivalTimeSlotIdOnSundays { get; set; }

        public Guid? ArrivalTimeSlotIdOnMondays { get; set; }

        public Guid? ArrivalTimeSlotIdOnTuesdays { get; set; }

        public Guid? ArrivalTimeSlotIdOnWednesdays { get; set; }

        public Guid? ArrivalTimeSlotIdOnThursdays { get; set; }

        public Guid? ArrivalTimeSlotIdOnFridays { get; set; }

        public Guid? ArrivalTimeSlotIdOnSaturdays { get; set; }

        public int InvoiceDateGenerationOptions { get; set; }

        public int InvoiceGroupByOptions { get; set; }

        [StringLength(50)]
        public string InvoiceGroupByTransportOrderColumnName { get; set; }

        [StringLength(50)]
        public string GeneralLedgerAccount { get; set; }

        public Guid? TransportUnitTransactionOverviewTextTemplateId { get; set; }

        public bool? SendFreightDocumentsAlongWithInvoice { get; set; }

        [StringLength(50)]
        public string CarrierCode { get; set; }

        [StringLength(50)]
        public string SupplyNumber { get; set; }

        public Guid? ThirdPartyToUseForInvoicing { get; set; }

        public int? Flags { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RelationAddress> RelationAddress { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RelationCategory> RelationCategory { get; set; }
    }
}
