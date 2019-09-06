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
    public class RelationsController : Controller
    {
        private testEntities db = new testEntities();

        private async Task<List<RelationModel>> InitRelationModels()
        {
            var relationModels = from tblRelation in db.tblRelation
                            join tblRelationAddress in db.tblRelationAddress on tblRelation.Id equals tblRelationAddress.RelationId
                            where tblRelation.IsDisabled != true
                            select new RelationModel
                            {
                                Id = tblRelation.Id,
                                RelationAddressId = tblRelationAddress.Id,
                                Name = tblRelation.Name,
                                FullName = tblRelation.FullName,
                                TelephoneNumber = tblRelation.TelephoneNumber,
                                Email = tblRelation.EMailAddress,
                                Country = tblRelationAddress.CountryName,
                                City = tblRelationAddress.City,
                                Street = tblRelationAddress.Street,
                                PostalCode = tblRelationAddress.PostalCode,
                                StreetNumber = tblRelationAddress.Number ?? 0
                            };

            return await relationModels.ToListAsync();
        }

        // GET: tblRelations
        public async Task<ActionResult> Index()
        {
            var relationModels = await InitRelationModels();

            return View(relationModels);
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
        public async Task<ActionResult> Create([Bind(Include = "Id,RelationAddressId,Name,FullName,Email,TelephoneNumber,Country,City,Street,PostalCode,StreetNumber")] RelationModel relationModel)
        {
            if (ModelState.IsValid)
            {
                var relation = new tblRelation
                {
                    Id = Guid.NewGuid(),
                    Name = relationModel.Name,
                    FullName = relationModel.FullName,
                    TelephoneNumber = relationModel.TelephoneNumber,
                    EMailAddress = relationModel.Email,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "admin",
                    IsDisabled = false,
                    IsTemporary = false,
                    IsMe = false,
                    PaymentViaAutomaticDebit = false,
                    InvoiceDateGenerationOptions = 0,
                    InvoiceGroupByOptions = 0
                };

                db.tblRelation.Add(relation);

                var relationAddress = new tblRelationAddress
                {
                    Id = Guid.NewGuid(),
                    RelationId = relationModel.Id,
                    CountryName = relationModel.Country,
                    City = relationModel.City,
                    Street = relationModel.Street,
                    PostalCode = relationModel.PostalCode,
                    Number = relationModel.StreetNumber,
                    AddressTypeId = Guid.Parse("00000000-0000-0000-0000-000000000002")
                };

                db.tblRelationAddress.Add(relationAddress);

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(relationModel);
        }

        // GET: tblRelations/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var tblRelation = await db.tblRelation.FindAsync(id);

            if (tblRelation == null)
            {
                return HttpNotFound();
            }

            var relationModels = await InitRelationModels();

            return View(relationModels.First(r => r.Id == tblRelation.Id));
        }

        // POST: tblRelations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,RelationAddressId,Name,FullName,Email,TelephoneNumber,Country,City,Street,PostalCode,StreetNumber")] RelationModel relationModel)
        {
            if (ModelState.IsValid)
            {
                tblRelation tblRelation = await db.tblRelation.FindAsync(relationModel.Id);

                tblRelation.Name = relationModel.Name;

                tblRelation.FullName = relationModel.FullName;

                tblRelation.EMailAddress = relationModel.Email;

                tblRelation.TelephoneNumber = relationModel.TelephoneNumber;

                tblRelationAddress tblRelationAddress = await db.tblRelationAddress.FindAsync(relationModel.RelationAddressId);

                tblRelationAddress.CountryName = relationModel.Country;

                tblRelationAddress.City = relationModel.City;

                tblRelationAddress.Street = relationModel.Street;
                
                //TODO: PostalCode mask
                tblRelationAddress.PostalCode = relationModel.PostalCode;

                tblRelationAddress.Number = relationModel.StreetNumber;

                db.Entry(tblRelation).State = EntityState.Modified;

                db.Entry(tblRelationAddress).State = EntityState.Modified;

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(relationModel);
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
            
            //no removing, checking IsDisabled only
            tblRelation.IsDisabled = true;

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
