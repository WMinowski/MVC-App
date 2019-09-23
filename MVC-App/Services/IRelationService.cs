using MVC_App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC_App.Services
{
    interface IRelationService
    {
        IRelationRepository Repository { get; set; }

        SelectList Categories { get; set; }

        SelectList Countries { get; set; }

        Task<List<RelationVM>> InitRelationModels();

        Task<RelationListVM> GetAsync(Guid? categoryId);

        Task Create(CreateEditRelationVM relationVM);

        Task Edit(CreateEditRelationVM relationVM);
    }
}
