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
using System.IO;

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
        public async Task<ActionResult> Index()
        {
            var list = await relationService.GetAsync();

            return View(list);
        }

        public async Task<ActionResult> RenderRelationPartialView(Guid? categoryId, string sortBy, string orderBy)
        {
            var list = await relationService.GetAsync(categoryId, sortBy, orderBy);

            return PartialView(list);
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
            string html;
            bool isValid = ModelState.IsValid;

            relationVM.Countries = relationService.Countries;

            if (!isValid)
            {
                html = RenderRazorViewToString("~/Views/Relations/Create.cshtml", relationVM);
            }
            else
            {
                await relationService.Create(relationVM);

                var relationList = await relationService.GetAsync();

                html = RenderRazorViewToString("~/Views/Shared/RenderRelationPartialView.cshtml", relationList);
            }

            return Json(new { isValid = isValid, html = html });
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
            string html;
            bool isValid = ModelState.IsValid;

            relationVM.Countries = relationService.Countries;

            if (!isValid)
            {
                html = RenderRazorViewToString("~/Views/Relations/Edit.cshtml", relationVM);
            }
            else
            {
                await relationService.Edit(relationVM);

                var relationList = await relationService.GetAsync();

                html = RenderRazorViewToString("~/Views/Shared/RenderRelationPartialView.cshtml", relationList);
            }

            return Json(new { isValid = isValid, html = html });
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

            var relationList = await relationService.GetAsync();

            var html = RenderRazorViewToString("~/Views/Shared/RenderRelationPartialView.cshtml", relationList);

            return Json(new { html = html });
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
