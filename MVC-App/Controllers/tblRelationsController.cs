using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_App.Domain.Models;

namespace MVC_App.Controllers
{
    public class tblRelationsController : Controller
    {
        private testEntities db = new testEntities();

        // GET: tblRelations
        public async Task<ActionResult> Index()
        {
            var relations = from tblRelation in db.tblRelation
                            join tblRelationAddress in db.tblRelationAddress on tblRelation.Id equals tblRelationAddress.RelationId
                select new RelationModel
                {
                    Id = tblRelation.Id,
                    Name = tblRelation.Name,
                    FullName = tblRelation.FullName,
                    TelephoneNumber = tblRelation.TelephoneNumber,
                    Email = tblRelation.EMailAddress,
                    Country = tblRelationAddress.CountryName,
                    City = tblRelationAddress.City,
                    Street = tblRelationAddress.Street,
                    PostalCode = tblRelationAddress.PostalCode,
                    StreetNumber = tblRelationAddress.Number??0
                };
            
            return View(await relations.ToListAsync());
        }

        // GET: tblRelations/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRelation tblRelation = await db.tblRelation.FindAsync(id);
            if (tblRelation == null)
            {
                return HttpNotFound();
            }
            return View(tblRelation);
        }

        // GET: tblRelations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblRelations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CreatedAt,CreatedBy,ModifiedAt,ModifiedBy,IsDisabled,ParentRelationId,IsTemporary,IsMe,Name,FullName,DepartureName,ArrivalName,DefaultStreet,DefaultPostalCode,DefaultCity,DefaultCountry,EMailAddress,Url,IMAddress,SkypeAddress,TelephoneNumber,MobileNumber,FaxNumber,EmergencyNumber,DepartureBetween,DepartureBetweenAnd,ArrivalBetween,ArrivalBetweenAnd,Remarks,CustomerCode,DebtorNumber,VendorNumber,InvoiceTo,InvoiceEMailAddress,SendInvoiceDigital,VatId,VatName,PaymentTerm,PaymentViaAutomaticDebit,VatNumber,ChamberOfCommerce,BankName,BankAccount,BankBic,CalculateMinimalPrice,CalculatePriceManually,CalculatePriceByPriceList,PriceListId,PriceListName,CalculatePriceBasedOnPositions,CalculatePriceBasedOnAmount,CalculatePriceBasedOnWeight,CalculatePriceBasedOnDistance,CalculatePriceBasedOnTonne,CalculatePriceByFixed,CalculatePriceByDistance,CalculatePriceBasedOnEpq,CalculatePriceBasedOnLoadingMeters,CalculatePriceBasedOnVolume,CalculatePriceByFixedPrice,CalculateMinimalPriceForCollecting,CalculatePriceManuallyForCollecting,CalculatePriceByPriceListForCollecting,PriceListIdForCollecting,PriceListNameForCollecting,CalculatePriceBasedOnPositionsForCollecting,CalculatePriceBasedOnAmountForCollecting,CalculatePriceBasedOnWeightForCollecting,CalculatePriceBasedOnDistanceForCollecting,CalculatePriceBasedOnTonneForCollecting,CalculatePriceByFixedForCollecting,CalculatePriceByDistanceForCollecting,CalculatePriceBasedOnEpqForCollecting,CalculatePriceBasedOnLoadingMetersForCollecting,CalculatePriceBasedOnVolumeForCollecting,CalculatePriceByFixedPriceForCollecting,GeographicalRegions,SendDigitalFreightDocumentsByEMail,DigitalFreightDocumentEMailTemplateId,SendFreightStatusUpdateByEMail,DepartureTimeSlotsAreAllEqual,DepartureTimeSlotIdOnSundays,DepartureTimeSlotIdOnMondays,DepartureTimeSlotIdOnTuesdays,DepartureTimeSlotIdOnWednesdays,DepartureTimeSlotIdOnThursdays,DepartureTimeSlotIdOnFridays,DepartureTimeSlotIdOnSaturdays,ArrivalTimeSlotsAreAllEqual,ArrivalTimeSlotIdOnSundays,ArrivalTimeSlotIdOnMondays,ArrivalTimeSlotIdOnTuesdays,ArrivalTimeSlotIdOnWednesdays,ArrivalTimeSlotIdOnThursdays,ArrivalTimeSlotIdOnFridays,ArrivalTimeSlotIdOnSaturdays,InvoiceDateGenerationOptions,InvoiceGroupByOptions,InvoiceGroupByTransportOrderColumnName,GeneralLedgerAccount,TransportUnitTransactionOverviewTextTemplateId,SendFreightDocumentsAlongWithInvoice,CarrierCode,SupplyNumber,ThirdPartyToUseForInvoicing,Flags")] tblRelation tblRelation)
        {
            if (ModelState.IsValid)
            {
                tblRelation.Id = Guid.NewGuid();
                db.tblRelation.Add(tblRelation);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tblRelation);
        }

        // GET: tblRelations/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRelation tblRelation = await db.tblRelation.FindAsync(id);
            if (tblRelation == null)
            {
                return HttpNotFound();
            }
            return View(tblRelation);
        }

        // POST: tblRelations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Name,FullName,EMailAddress,TelephoneNumber")] tblRelation tblRelation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblRelation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tblRelation);
        }

        // GET: tblRelations/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRelation tblRelation = await db.tblRelation.FindAsync(id);
            if (tblRelation == null)
            {
                return HttpNotFound();
            }
            return View(tblRelation);
        }

        // POST: tblRelations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            tblRelation tblRelation = await db.tblRelation.FindAsync(id);
            //TODO: no removing, checking IsDisabled only
            db.tblRelation.Remove(tblRelation);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
