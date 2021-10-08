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
    public class DistributorBusinessLogic : BusinessLogicBase
    {
        public DistributorViewModel GetDistributorViewModel(int distributorId, DistributorViewModel viewModel = null)
        {
            viewModel = viewModel ?? new DistributorViewModel();

            var distrib = db.Distributors.FirstOrDefault(c => c.DistributorId == distributorId);
            if (distrib != null)
            {
                Transform.FromObjectToObject(distrib, viewModel);

                viewModel.ManufacturerIds = db.DistributorManufacturers.Where(c => c.DistributorId == distrib.DistributorId).Select(c => c.ManufacturerId).ToList();
            }

            return viewModel;
        }

        public DataSourceResult GetDistributors(DataSourceRequest request)
        {
            Hashtable fltr = new Hashtable();
            Common.CommonFunction.PopulateFiltersInHashTable(request.Filters, fltr);

            string sortBy = string.Empty;
            if (request.Sorts.Any())
                sortBy = request.Sorts[0].Member + " " + request.Sorts[0].SortDirection;

            ObjectParameter objparam = new ObjectParameter("TotalRecords", System.Data.DbType.Int16);

            var name = fltr.ContainsKey("Name") ? fltr["Name"].ToString() : string.Empty;
            var company = fltr.ContainsKey("Company") ? fltr["Company"].ToString() : null;
            var phone = fltr.ContainsKey("Phone") ? fltr["Phone"].ToString() : null;

            var queryResult = db.Distributor_Get(name, company, phone, request.Page, request.PageSize, sortBy, objparam).ToList()
                .Select(d => new DistributorViewModel()
                {
                    DistributorId = d.DistributorId,
                    Name = d.Name,
                    Company = d.Company,
                    Address = d.Address,
                    City = d.City,
                    IsActive = d.IsActive,
                    Status = (d.IsActive) ? "Active" : "Inactive",
                    Phone = d.Phone,
                    EditDistributorUrl = CryptographyUtility.GetEncryptedQueryString(new { distributId = d.DistributorId })
                });

            DataSourceResult dsr = new DataSourceResult();
            dsr.Data = queryResult;
            dsr.Total = (int)objparam.Value;

            return dsr;
        }

        public void SaveDistributor(DistributorViewModel model)
        {
            var distrib = new Distributor();
            if (model.DistributorId > 0)
            {
                distrib = db.Distributors.FirstOrDefault(m => m.DistributorId == model.DistributorId);
            }

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                distrib.Name = model.Name;
                distrib.IsActive = true;
                distrib.Phone = string.IsNullOrEmpty(model.Phone) ? "" : CommonBusinessLogic.GetDigits(model.Phone);
                distrib.Address = model.Address;
                distrib.City = model.City;
                distrib.Company = model.Company;

                if (model.DistributorId == 0)
                {
                    distrib.CreatedBy = (int)GetLoggedInUserId();
                    distrib.CreatedOn = DateTime.UtcNow;

                    db.Distributors.Add(distrib);
                }
                else
                {
                    distrib.UpdatedBy = (int)GetLoggedInUserId();
                    distrib.UpdatedOn = DateTime.UtcNow;

                    db.Entry(distrib).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();

                db.DistributorManufacturers.RemoveRange(db.DistributorManufacturers.Where(x => x.DistributorId == distrib.DistributorId));

                foreach (var manu in model.ManufacturerIds)
                {

                    var distribmanufact = new DistributorManufacturer();

                    distribmanufact.DistributorId = distrib.DistributorId;
                    distribmanufact.ManufacturerId = manu;

                    db.DistributorManufacturers.Add(distribmanufact);
                    db.SaveChanges();
                }

                dbContextTransaction.Commit();

            }
        }

        public void ChangeDistributorStatus(int distribId)
        {
            ToggleActiveStatus<Distributor>(sub => sub.DistributorId == distribId);
        }

    }
}