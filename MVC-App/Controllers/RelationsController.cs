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
using MVC_App.Services;
using Ninject;

namespace MVC_App.Controllers
{
    public class RelationsController : Controller
    {
        private readonly IRelationService relationService;
        
        public RelationsController()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRelationService>().To<RelationService>();
            relationService = ninjectKernel.Get<IRelationService>();
        }
        
        // GET: Relations
        public async Task<ActionResult> Index(Guid? categoryId, string sortOrder)
        {
            var list = await relationService.GetAsync(categoryId);

            list.SortOrderByName = sortOrder == "Name" ? "Name desc" : "Name";
            list.SortOrderByFullName = sortOrder == "FullName" ? "FullName desc" : "FullName";
            list.SortOrderByTelephoneNumber = sortOrder == "TelephoneNumber" ? "TelephoneNumber desc" : "TelephoneNumber";
            list.SortOrderByEmail = sortOrder == "Email" ? "Email desc" : "Email";
            list.SortOrderByCountry = sortOrder == "Country" ? "Country desc" : "Country";
            list.SortOrderByCity = sortOrder == "City" ? "City desc" : "City";
            list.SortOrderByStreet = sortOrder == "Street" ? "Street desc" : "Street";
            list.SortOrderByPostalCode = sortOrder == "PostalCode" ? "PostalCode desc" : "PostalCode";

            switch (sortOrder)
            {
                case "Name":
                    list.RelationViewModels = list.RelationViewModels.OrderBy(s => s.Name);
                    break;
                case "Name desc":
                    list.RelationViewModels = list.RelationViewModels.OrderByDescending(s => s.Name);
                    break;
                case "FullName":
                    list.RelationViewModels = list.RelationViewModels.OrderBy(s => s.FullName);
                    break;
                case "FullName desc":
                    list.RelationViewModels = list.RelationViewModels.OrderByDescending(s => s.FullName);
                    break;
                case "TelephoneNumber":
                    list.RelationViewModels = list.RelationViewModels.OrderBy(s => s.TelephoneNumber);
                    break;
                case "TelephoneNumber desc":
                    list.RelationViewModels = list.RelationViewModels.OrderByDescending(s => s.TelephoneNumber);
                    break;
                case "Email":
                    list.RelationViewModels = list.RelationViewModels.OrderBy(s => s.Email);
                    break;
                case "Email desc":
                    list.RelationViewModels = list.RelationViewModels.OrderByDescending(s => s.Email);
                    break;
                case "Country":
                    list.RelationViewModels = list.RelationViewModels.OrderBy(s => s.CountryId);
                    break;
                case "Country desc":
                    list.RelationViewModels = list.RelationViewModels.OrderByDescending(s => s.CountryId);
                    break;
                case "City":
                    list.RelationViewModels = list.RelationViewModels.OrderBy(s => s.City);
                    break;
                case "City desc":
                    list.RelationViewModels = list.RelationViewModels.OrderByDescending(s => s.City);
                    break;
                case "Street":
                    list.RelationViewModels = list.RelationViewModels.OrderBy(s => s.Street);
                    break;
                case "Street desc":
                    list.RelationViewModels = list.RelationViewModels.OrderByDescending(s => s.Street);
                    break;
                case "PostalCode":
                    list.RelationViewModels = list.RelationViewModels.OrderBy(s => s.PostalCode);
                    break;
                case "PostalCode desc":
                    list.RelationViewModels = list.RelationViewModels.OrderByDescending(s => s.PostalCode);
                    break;
                default:
                    list.RelationViewModels = list.RelationViewModels.OrderBy(s => s.Name);
                    break;
            }

            return View(list);
        }

        // GET: Relations/Create
        public ActionResult Create()
        {
            var relationVM = new CreateEditRelationVM { Countries = relationService.Countries };

            return View(relationVM);
        }

        // POST: Relations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Relation,Countries")] CreateEditRelationVM relationVM)
        {
            if (!ModelState.IsValid)
            {
                relationVM.Countries = relationService.Countries;

                return View(relationVM);
            }

            await relationService.Create(relationVM);

            return RedirectToAction("Index");
        }

        // GET: Relations/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var relation = await relationService.GetRelationAsync(id);

            if (relation == null)
            {
                return HttpNotFound();
            }

            var relationModels = await relationService.InitRelationModels();

            var editRelationVM = new CreateEditRelationVM { Relation = relationModels.First(r => r.Id == relation.Id), Countries = relationService.Countries };

            return View(editRelationVM);
        }

        // POST: Relations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Relation,Countries")] CreateEditRelationVM relationVM)
        {
            if (ModelState.IsValid)
            {
                await relationService.Edit(relationVM);

                return RedirectToAction("Index");
            }
            return View(relationVM);
        }

        // GET: Relations/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var relation = await relationService.GetRelationAsync(id);

            if (relation == null)
            {
                return HttpNotFound();
            }

            return View(relation);
        }

        // POST: Relations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            await relationService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
