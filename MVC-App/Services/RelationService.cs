using MVC_App.Domain.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC_App.Services
{
    public class RelationService : IRelationService
    {
        public IRelationRepository Repository { get; set; }

        public SelectList Categories { get; set; }

        public SelectList Countries { get; set; }

        public RelationService()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRelationRepository>().To<RelationRepository>();
            Repository = ninjectKernel.Get<IRelationRepository>();

            var categories = Repository.DbContext.Categories.ToList();

            categories.Insert(0, new Category
            {
                Id = Guid.Empty,
                CreatedAt = DateTime.Now,
                CreatedBy = "admin",
                IsDisabled = false,
                Name = "Все"
            });

            Categories = new SelectList(categories, "Id", "Name");

            var countries = Repository.DbContext.Countries.ToList();

            Countries = new SelectList(countries, "Id", "Name");
        }

        public async Task<List<RelationVM>> InitRelationModels()
        {
            

            var relationModels = from tblRelation in Repository.DbContext.Relations
                                 join tblRelationAddress in Repository.DbContext.RelationAddresses on tblRelation.Id equals tblRelationAddress.RelationId
                                 join tblCountry in Repository.DbContext.Countries on tblRelationAddress.CountryId equals tblCountry.Id
                                 where !tblRelation.IsDisabled
                                 select new RelationVM
                                 {
                                     Id = tblRelation.Id,
                                     RelationAddressId = tblRelationAddress.Id,
                                     Categories = (from tblRelationCategory in Repository.DbContext.RelationCategories where tblRelationCategory.RelationId == tblRelation.Id select tblRelationCategory.CategoryId).ToList(),
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
            if (mask == string.Empty || mask == null || value == string.Empty) return value;

            List<char> result = new List<char>();

            int valueIterator = 0;

            int valueNumbersCount = 0;

            int valueLettersCount = 0;

            int maskNumbersCount = 0;

            int maskLettersCount = 0;

            foreach (char c in value)
            {
                if (char.IsDigit(c))
                {
                    valueNumbersCount++;
                }
                else if (char.IsLetter(c))
                {
                    valueLettersCount++;
                }
            }

            foreach (char c in mask)
            {
                if (c == 'N')
                {
                    maskNumbersCount++;
                }
                else if (c == 'L' || c == 'l')
                {
                    maskLettersCount++;
                }
            }

            if (valueNumbersCount != maskNumbersCount || valueLettersCount != maskLettersCount)
            {
                return value;
            }

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

        public async Task<RelationListVM> GetAsync(Guid? categoryId)
        {
            var countries = Repository.DbContext.Countries.ToList();

            var relationModels = await InitRelationModels();

            if (categoryId != null && categoryId != Guid.Empty)
            {
                relationModels = relationModels.Where(p => p.Categories.Contains(categoryId.Value)).ToList();
            }

            var relationListVM = new RelationListVM { RelationViewModels = relationModels, Categories = Categories, Countries = countries };

            return relationListVM;
        }

        public async Task Create(CreateEditRelationVM relationVM)
        {
            var relation = new Relation
            {
                Id = Guid.NewGuid(),
                Name = relationVM.Relation.Name,
                FullName = relationVM.Relation.FullName,
                TelephoneNumber = relationVM.Relation.TelephoneNumber,
                EMailAddress = relationVM.Relation.Email,
                CreatedAt = DateTime.Now,
                CreatedBy = "admin",
                IsDisabled = false,
                IsTemporary = false,
                IsMe = false,
                PaymentViaAutomaticDebit = false,
                InvoiceDateGenerationOptions = 0,
                InvoiceGroupByOptions = 0
            };

            Repository.DbContext.Relations.Add(relation);

            var relationAddress = new RelationAddress
            {
                Id = Guid.NewGuid(),
                RelationId = relation.Id,
                CountryId = relationVM.Relation.CountryId,
                City = relationVM.Relation.City,
                Street = relationVM.Relation.Street,
                PostalCode = ApplyMask(relationVM.Relation.PostalCode, Repository.DbContext.Countries.Find(relationVM.Relation.CountryId).PostalCodeFormat),
                Number = relationVM.Relation.StreetNumber,
                AddressTypeId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            };

            Repository.DbContext.RelationAddresses.Add(relationAddress);

            await Repository.DbContext.SaveChangesAsync();
        }

        public async Task Edit(CreateEditRelationVM relationVM)
        {
            Relation tblRelation = await Repository.DbContext.Relations.FindAsync(relationVM.Relation.Id);

            tblRelation.Name = relationVM.Relation.Name;

            tblRelation.FullName = relationVM.Relation.FullName;

            tblRelation.EMailAddress = relationVM.Relation.Email;

            tblRelation.TelephoneNumber = relationVM.Relation.TelephoneNumber;

            RelationAddress tblRelationAddress = await Repository.DbContext.RelationAddresses.FindAsync(relationVM.Relation.RelationAddressId);

            tblRelationAddress.CountryId = relationVM.Relation.CountryId;

            tblRelationAddress.City = relationVM.Relation.City;

            tblRelationAddress.Street = relationVM.Relation.Street;

            tblRelationAddress.PostalCode = ApplyMask(relationVM.Relation.PostalCode, Repository.DbContext.Countries.Find(relationVM.Relation.CountryId).PostalCodeFormat);

            tblRelationAddress.Number = relationVM.Relation.StreetNumber;

            Repository.DbContext.Entry(tblRelation).State = EntityState.Modified;

            Repository.DbContext.Entry(tblRelationAddress).State = EntityState.Modified;

            await Repository.DbContext.SaveChangesAsync();
        }
    }
}