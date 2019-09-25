using MVC_App.Domain.Models;
using MVC_App.Repositories;
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
        private readonly UnitOfWork _unitOfWork;

        public SelectList Categories { get; set; }

        public SelectList Countries { get; set; }

        public RelationService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var categories = unitOfWork.CategoryRepository.Get().ToList();

            categories.Insert(0, new Category
            {
                Id = Guid.Empty,
                CreatedAt = DateTime.Now,
                CreatedBy = "admin",
                IsDisabled = false,
                Name = "Все"
            });

            Categories = new SelectList(categories, "Id", "Name");

            var countries = unitOfWork.CountryRepository.Get().ToList();

            Countries = new SelectList(countries, "Id", "Name");
        }

        public async Task<List<RelationVM>> InitRelationModels()
        {
            

            var relationModels = from relation in _unitOfWork.RelationRepository.Get()
                                 join relationAddress in _unitOfWork.RelationAddressRepository.Get() on relation.Id equals relationAddress.RelationId
                                 join country in _unitOfWork.CountryRepository.Get() on relationAddress.CountryId equals country.Id
                                 where !relation.IsDisabled
                                 select new RelationVM
                                 {
                                     Id = relation.Id,
                                     RelationAddressId = relationAddress.Id,
                                     Categories = (from relationCategory in _unitOfWork.RelationCategoryRepository.Get() where relationCategory.RelationId == relation.Id select relationCategory.CategoryId).ToList(),
                                     Name = relation.Name,
                                     FullName = relation.FullName,
                                     TelephoneNumber = relation.TelephoneNumber,
                                     Email = relation.EMailAddress,
                                     CountryId = country.Id,
                                     CountryName = country.Name,
                                     City = relationAddress.City,
                                     Street = relationAddress.Street,
                                     PostalCode = relationAddress.PostalCode,
                                     PostalCodeMask = country.PostalCodeFormat,
                                     StreetNumber = relationAddress.Number ?? 0
                                 };
            return relationModels.ToList();
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

        public async Task<Relation> GetRelationAsync(Guid? id)
        {
            return _unitOfWork.RelationRepository.GetByID(id);
        }

        public async Task<RelationListVM> GetAsync(Guid? categoryId)
        {
            var countries = _unitOfWork.CountryRepository.Get().ToList();

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

            _unitOfWork.RelationRepository.Insert(relation);

            var relationAddress = new RelationAddress
            {
                Id = Guid.NewGuid(),
                RelationId = relation.Id,
                CountryId = relationVM.Relation.CountryId,
                City = relationVM.Relation.City,
                Street = relationVM.Relation.Street,
                PostalCode = ApplyMask(relationVM.Relation.PostalCode, _unitOfWork.CountryRepository.GetByID(relationVM.Relation.CountryId).PostalCodeFormat),
                Number = relationVM.Relation.StreetNumber,
                AddressTypeId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            };

            _unitOfWork.RelationAddressRepository.Insert(relationAddress);

            await _unitOfWork.SaveAsync();
        }

        public async Task Edit(CreateEditRelationVM relationVM)
        {
            Relation relation = _unitOfWork.RelationRepository.GetByID(relationVM.Relation.Id);

            relation.Name = relationVM.Relation.Name;

            relation.FullName = relationVM.Relation.FullName;

            relation.EMailAddress = relationVM.Relation.Email;

            relation.TelephoneNumber = relationVM.Relation.TelephoneNumber;

            RelationAddress relationAddress = _unitOfWork.RelationAddressRepository.GetByID(relationVM.Relation.RelationAddressId);

            relationAddress.CountryId = relationVM.Relation.CountryId;

            relationAddress.City = relationVM.Relation.City;

            relationAddress.Street = relationVM.Relation.Street;

            relationAddress.PostalCode = ApplyMask(relationVM.Relation.PostalCode, _unitOfWork.CountryRepository.GetByID(relationVM.Relation.CountryId).PostalCodeFormat);

            relationAddress.Number = relationVM.Relation.StreetNumber;

            _unitOfWork.RelationRepository.Update(relation);

            _unitOfWork.RelationAddressRepository.Update(relationAddress);

            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(Guid id)
        {
            var relation = _unitOfWork.RelationRepository.GetByID(id);

            //no removing, checking IsDisabled only
            relation.IsDisabled = true;

            await _unitOfWork.SaveAsync();
        }
    }
}