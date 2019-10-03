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
        public async Task<ActionResult> Index(Guid? categoryId, string sortOrder, bool? IsDescOrder)
        {
            var list = await relationService.GetAsync(categoryId, sortOrder, IsDescOrder ?? false);

            list.IsDescOrder = !IsDescOrder ?? false;

            return View(list);
        }

        // GET: Relations/Create
        public ActionResult Create()
        {
            var relationVM = new CreateEditRelationVM { Countries = relationService.Countries };

            return PartialView(relationVM);
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

            return PartialView(editRelationVM);
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
            return PartialView(relationVM);
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

            return PartialView(relation);
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
