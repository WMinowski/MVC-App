using MVC_App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC_App.Services
{
    public interface IRelationService
    {
        SelectList Categories { get; set; }

        SelectList Countries { get; set; }

        string ApplyMask(string value, string mask);

        Task<Relation> GetRelationAsync(Guid? id);

        Task<List<RelationVM>> InitRelationModels();

        Task<RelationListVM> GetAsync(Guid? categoryId, string sortOrder, bool IsDescOrder);

        Task Create(CreateEditRelationVM relationVM);

        Task Edit(CreateEditRelationVM relationVM);

        Task Delete(Guid id);
    }
}
