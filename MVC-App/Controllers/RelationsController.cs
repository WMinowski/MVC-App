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

        private SelectList categoryFilter;

        public async Task<List<RelationViewModel>> InitRelationModels()
        {
            var categories = db.tblCategory.ToList();

            categories.Insert(0, new tblCategory
            {
                Id = Guid.Empty,
                CreatedAt = DateTime.Now,
                CreatedBy = "admin",
                IsDisabled = false,
                Name = "Все"
            });

            categoryFilter = new SelectList(categories, "Id", "Name");

            var relationModels = from tblRelation in db.tblRelation
                            join tblRelationAddress in db.tblRelationAddress on tblRelation.Id equals tblRelationAddress.RelationId
                            join tblCountry in db.tblCountry on tblRelationAddress.CountryId equals tblCountry.Id
                            where tblRelation.IsDisabled != true
                            select new RelationViewModel
                            {
                                Id = tblRelation.Id,
                                RelationAddressId = tblRelationAddress.Id,
                                Categories = (from tblRelationCategory in db.tblRelationCategory where tblRelationCategory.RelationId == tblRelation.Id select tblRelationCategory.CategoryId).ToList(),
                                Name = tblRelation.Name,
                                FullName = tblRelation.FullName,
                                TelephoneNumber = tblRelation.TelephoneNumber,
                                Email = tblRelation.EMailAddress,
                                CountryId = tblCountry.Id,
                                CountryName = tblCountry.Name,
                                City = tblRelationAddress.City,
                                Street = tblRelationAddress.Street,
                                PostalCode = tblRelationAddress.PostalCode,
                                PostalCodeMask = tblCountry.PostalCodeFormat,
                                StreetNumber = tblRelationAddress.Number ?? 0
                            };
            return await relationModels.ToListAsync();
        }

        public string ApplyMask(string value, string mask)
        {
            if (mask == string.Empty||mask == null||value == string.Empty) return value;

            List<char> result = new List<char>();

            int valueIterator = 0;

            for (int i = 0; i < mask.Length; i++)
            {
                switch (mask[i])
                {
                    case 'N':
                        {
                            if (char.IsDigit(value[valueIterator]))
                            {
                                result.Add(value[valueIterator]);

                                valueIterator++;
                            }
                            else return value;

                            break;
                        }
                    case 'L':
                        {
                            if (char.IsLetter(value[valueIterator]))
                            {
                                result.Add(char.ToUpper(value[valueIterator]));

                                valueIterator++;
                            }
                            else return value;

                            break;
                        }
                    case 'l':
                        {
                            if (char.IsLetter(value[valueIterator]))
                            {
                                result.Add(char.ToLower(value[valueIterator]));

                                valueIterator++;
                            }
                            else return value;

                            break;
                        }
                    default:
                        {
                            result.Add(mask[i]);
                            if (!char.IsLetterOrDigit(value[valueIterator]))
                            {
                                if (value[valueIterator] == mask[i])
                                {
                                    valueIterator++;
                                }
                                else return value;
                            }
                            break;
                        }
                }
            }
            if (value.Length == valueIterator)
            {
                return new string(result.ToArray());
            }
            else return value;
        }

        // GET: tblRelations
        public async Task<ActionResult> Index(Guid? categoryId)
        {
            var countries = db.tblCountry.ToList();

            var relationModels = await InitRelationModels();

            if(categoryId != null && categoryId != Guid.Empty)
            {
                relationModels = relationModels.Where(p => p.Categories.Contains(categoryId.Value)).ToList();
            }

            var relationListVM = new RelationListViewModel { RelationViewModels = relationModels, Categories = categoryFilter, Countries = countries };

            return View(relationListVM);
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
            var relationVM = new CreateEditRelationViewModel { Countries = new SelectList(db.tblCountry.ToList(), "Id", "Name") };

            return View(relationVM);
        }

        // POST: tblRelations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Relation,Countries")] CreateEditRelationViewModel relationModel)
        {
            if (ModelState.IsValid)
            {
                var relation = new tblRelation
                {
                    Id = Guid.NewGuid(),
                    Name = relationModel.Relation.Name,
                    FullName = relationModel.Relation.FullName,
                    TelephoneNumber = relationModel.Relation.TelephoneNumber,
                    EMailAddress = relationModel.Relation.Email,
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
                    RelationId = relation.Id,
                    CountryId = relationModel.Relation.CountryId,
                    City = relationModel.Relation.City,
                    Street = relationModel.Relation.Street,
                    PostalCode = ApplyMask(relationModel.Relation.PostalCode, db.tblCountry.Find(relationModel.Relation.CountryId).PostalCodeFormat),
                    Number = relationModel.Relation.StreetNumber,
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

            var editRelationVM = new CreateEditRelationViewModel { Relation = relationModels.First(r => r.Id == tblRelation.Id), Countries = new SelectList(db.tblCountry.ToList(), "Id", "Name") };
            return View(editRelationVM);
        }

        // POST: tblRelations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Relation,Countries")] CreateEditRelationViewModel relationModel)
        {

            if (ModelState.IsValid)
            {
                tblRelation tblRelation = await db.tblRelation.FindAsync(relationModel.Relation.Id);

                tblRelation.Name = relationModel.Relation.Name;

                tblRelation.FullName = relationModel.Relation.FullName;

                tblRelation.EMailAddress = relationModel.Relation.Email;

                tblRelation.TelephoneNumber = relationModel.Relation.TelephoneNumber;

                tblRelationAddress tblRelationAddress = await db.tblRelationAddress.FindAsync(relationModel.Relation.RelationAddressId);

                tblRelationAddress.CountryId = relationModel.Relation.CountryId;

                tblRelationAddress.City = relationModel.Relation.City;

                tblRelationAddress.Street = relationModel.Relation.Street;
                
                tblRelationAddress.PostalCode = ApplyMask(relationModel.Relation.PostalCode, db.tblCountry.Find(relationModel.Relation.CountryId).PostalCodeFormat);

                tblRelationAddress.Number = relationModel.Relation.StreetNumber;

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
