using AutoMapper;
using AutoMapper.QueryableExtensions;
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
                                     Categories = (from relationCategory in _unitOfWork.RelationCategoryRepository.Get()
                                                   where relationCategory.RelationId == relation.Id
                                                   select relationCategory.CategoryId)
                                                   .ToList(),
                                     Name = relation.Name,
                                     FullName = relation.FullName,
                                     TelephoneNumber = relation.TelephoneNumber,
                                     Email = relation.EMailAddress,
                                     CountryId = country.Id,
                                     CountryName = country.Name,
                                     City = relationAddress.City,
                                     Street = relationAddress.Street,
                                     PostalCode = relationAddress.PostalCode,
                                     StreetNumber = relationAddress.Number ?? 0
                                 };
            return relationModels.ToList();
        }

        public string ApplyMask(string value, string mask)
        {
            if (mask == string.Empty || mask == null || value == string.Empty) return value;

            List<char> result = new List<char>();

            int valueIterator = 0, valueNumbersCount = 0, valueLettersCount = 0, maskNumbersCount = 0, maskLettersCount = 0;

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

        public async Task<RelationListVM> GetAsync(Guid? categoryId, string sortOrder, bool IsDescOrder)
        {
            var countries = _unitOfWork.CountryRepository.Get().ToList();

            var relationModels = await InitRelationModels();

            if (categoryId != null && categoryId != Guid.Empty)
            {
                relationModels = relationModels.Where(p => p.Categories.Contains(categoryId.Value)).ToList();
            }

            var relationListVM = new RelationListVM { RelationViewModels = relationModels, Categories = Categories, Countries = countries };

            switch (sortOrder)
            {
                case "Name":
                    relationListVM.RelationViewModels = !IsDescOrder
                        ? relationListVM.RelationViewModels.OrderBy(s => s.Name)
                        : relationListVM.RelationViewModels.OrderByDescending(s => s.Name);
                    break;
                case "FullName":
                    relationListVM.RelationViewModels = !IsDescOrder
                        ? relationListVM.RelationViewModels.OrderBy(s => s.FullName)
                        : relationListVM.RelationViewModels.OrderByDescending(s => s.FullName);
                    break;
                case "TelephoneNumber":
                    relationListVM.RelationViewModels = !IsDescOrder 
                        ? relationListVM.RelationViewModels.OrderBy(s => s.TelephoneNumber)
                        : relationListVM.RelationViewModels.OrderByDescending(s => s.TelephoneNumber);
                    break;
                case "Email":
                    relationListVM.RelationViewModels = !IsDescOrder 
                        ? relationListVM.RelationViewModels.OrderBy(s => s.Email)
                        : relationListVM.RelationViewModels.OrderByDescending(s => s.Email);
                    break;
                case "Country":
                    relationListVM.RelationViewModels = !IsDescOrder 
                        ? relationListVM.RelationViewModels.OrderBy(s => s.CountryId)
                        : relationListVM.RelationViewModels.OrderByDescending(s => s.CountryId);
                    break;
                case "City":
                    relationListVM.RelationViewModels = !IsDescOrder 
                        ? relationListVM.RelationViewModels.OrderBy(s => s.City)
                        : relationListVM.RelationViewModels.OrderByDescending(s => s.City);
                    break;
                case "Street":
                    relationListVM.RelationViewModels = !IsDescOrder
                        ? relationListVM.RelationViewModels.OrderBy(s => s.Street)
                        : relationListVM.RelationViewModels.OrderByDescending(s => s.Street);
                    break;
                case "PostalCode":
                    relationListVM.RelationViewModels = !IsDescOrder
                        ? relationListVM.RelationViewModels.OrderBy(s => s.PostalCode)
                        : relationListVM.RelationViewModels.OrderByDescending(s => s.PostalCode);
                    break;
                default:
                    relationListVM.RelationViewModels = relationListVM.RelationViewModels.OrderBy(s => s.Name);
                    break;
            }

            return relationListVM;
        }

        public async Task Create(CreateEditRelationVM relationVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RelationVM, Relation>()
            .AfterMap((src, dest) => {
                dest.Id = Guid.NewGuid();
                dest.EMailAddress = src.Email;
                dest.CreatedAt = DateTime.Now;
                dest.CreatedBy = "admin";
                dest.IsDisabled = false;
                dest.IsTemporary = false;
                dest.IsMe = false;
                dest.PaymentViaAutomaticDebit = false;
                dest.InvoiceDateGenerationOptions = 0;
                dest.InvoiceGroupByOptions = 0;
            }
            ));

            var mapper = config.CreateMapper();

            var relation = mapper.Map<Relation>(relationVM.Relation);

            _unitOfWork.RelationRepository.Insert(relation);

            config = new MapperConfiguration(cfg => cfg.CreateMap<RelationVM, RelationAddress>()
            .AfterMap((src, dest) => {
                dest.Id = Guid.NewGuid();
                dest.RelationId = relation.Id;
                dest.PostalCode = ApplyMask(src.PostalCode, _unitOfWork.CountryRepository.GetByID(relationVM.Relation.CountryId).PostalCodeFormat);
                dest.Number = src.StreetNumber;
                dest.AddressTypeId = Guid.Parse("00000000-0000-0000-0000-000000000002");
            }
            ));

            mapper = config.CreateMapper();

            var relationAddress = mapper.Map<RelationAddress>(relationVM.Relation);

            _unitOfWork.RelationAddressRepository.Insert(relationAddress);

            await _unitOfWork.SaveAsync();
        }

        public async Task Edit(CreateEditRelationVM relationVM)
        {
            var relation = _unitOfWork.RelationRepository.GetByID(relationVM.Relation.Id);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<RelationVM, Relation>()
            .ForMember("Id", opt => opt.Ignore())
            .AfterMap((src, dest) => {
                dest.EMailAddress = src.Email;
                dest.CreatedAt = DateTime.Now;
                dest.CreatedBy = "admin";
                dest.IsDisabled = false;
                dest.IsTemporary = false;
                dest.IsMe = false;
                dest.PaymentViaAutomaticDebit = false;
                dest.InvoiceDateGenerationOptions = 0;
                dest.InvoiceGroupByOptions = 0;
            }
            ));

            var mapper = config.CreateMapper();

            mapper.Map<RelationVM, Relation>(relationVM.Relation, relation);

            var relationAddress = _unitOfWork.RelationAddressRepository.GetByID(relationVM.Relation.RelationAddressId);

            config = new MapperConfiguration(cfg => cfg.CreateMap<RelationVM, RelationAddress>()
            .ForMember("Id", opt => opt.Ignore())
            .AfterMap((src, dest) => {
                dest.PostalCode = ApplyMask(src.PostalCode, _unitOfWork.CountryRepository.GetByID(relationVM.Relation.CountryId).PostalCodeFormat);
                dest.Number = src.StreetNumber;
                dest.AddressTypeId = Guid.Parse("00000000-0000-0000-0000-000000000002");
            }
            ));

            mapper = config.CreateMapper();

            mapper.Map<RelationVM, RelationAddress>(relationVM.Relation, relationAddress);

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