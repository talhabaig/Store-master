using BaigMedicalStore.Common;
using BaigMedicalStore.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace BaigMedicalStore.BusinessLogic
{
    public class ManufacturerBusinessLogic : BusinessLogicBase
    { 
        public ManufacturerViewModel GetManufacturerViewModel(int manufacturerId, ManufacturerViewModel viewModel = null)
        {
            viewModel = viewModel ?? new ManufacturerViewModel();

            var manufact = db.Manufacturers.FirstOrDefault(c => c.ManufacturerId == manufacturerId);
            if (manufact != null)
            {
                Transform.FromObjectToObject(manufact, viewModel);
                viewModel.ManufactId = manufact.ManufacturerId;
            }
            viewModel.Country = "Pakistan";
            return viewModel;
        }

        public DataSourceResult GetManufacturers(DataSourceRequest request)
        {
            Hashtable fltr = new Hashtable();
            Common.CommonFunction.PopulateFiltersInHashTable(request.Filters, fltr);

            string sortBy = string.Empty;
            if (request.Sorts.Any())
                sortBy = request.Sorts[0].Member + " " + request.Sorts[0].SortDirection;

            ObjectParameter objparam = new ObjectParameter("TotalRecords", System.Data.DbType.Int16);

            var name = fltr.ContainsKey("Name") ? fltr["Name"].ToString() : string.Empty;
            var alias = fltr.ContainsKey("Alias") ? fltr["Alias"].ToString() : null;
            var phone = fltr.ContainsKey("Phone") ? fltr["Phone"].ToString() : null;

            var queryResult = db.Manufacturer_Get(name, alias, phone, request.Page, request.PageSize, sortBy, objparam).ToList()
                .Select(m => new ManufacturerViewModel()
                {
                    ManufactId = m.ManufacturerId,
                    Name = m.Name,
                    Alias = m.Alias,
                    Address = m.Address,
                    City = m.City,
                    Country = m.Country,
                    Status = (m.IsActive) ? "Active" : "Inactive",
                    IsActive = m.IsActive,
                    Phone = m.Phone,
                    EditManufacturerUrl = CryptographyUtility.GetEncryptedQueryString(new { manufactId = m.ManufacturerId })

                });

            DataSourceResult dsr = new DataSourceResult();
            dsr.Data = queryResult;
            dsr.Total = (int)objparam.Value;

            return dsr;
        }

        public void SaveManufacturer(ManufacturerViewModel model)
        {
            var manufact = new Manufacturer();
            if (model.ManufactId > 0)
            {
                manufact = db.Manufacturers.FirstOrDefault(m => m.ManufacturerId == model.ManufactId);
            }

            manufact.Name = model.Name;
            manufact.IsActive = true;
            manufact.Phone = model.Phone;
            manufact.Address = model.Address;
            manufact.City = model.City;
            manufact.Country = model.Country;
            manufact.Alias = model.Alias;

            if (model.ManufactId == 0)
            {
                manufact.CreatedBy = (int)GetLoggedInUserId();
                manufact.CreatedOn = DateTime.UtcNow;

                db.Manufacturers.Add(manufact);
            }
            else
            {
                manufact.UpdatedBy = (int)GetLoggedInUserId();
                manufact.UpdatedOn = DateTime.UtcNow;

                db.Entry(manufact).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
        }

        public void ChangeManufacturerStatus(int manufactId)
        {
            ToggleActiveStatus<Manufacturer>(sub => sub.ManufacturerId == manufactId);
        }
    }
}