using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaigMedicalStore.BusinessLogic
{
    public class CommonBusinessLogic : BusinessLogicBase
    {
        public List<SelectListItem> GetCategoryList()
        {
            var list = db.Categories
                .Select(item => new SelectListItem()
                {
                    Text = item.Name.ToString(),
                    Value = item.CategoryId.ToString()
                }
                ).ToList();

            return list;
        }

        public List<SelectListItem> GetLocationList()
        {
            var list = db.Locations
                 .Select(item => new SelectListItem()
                 {
                     Text = item.Name.ToString(),
                     Value = item.LocationId.ToString()
                 }
                 ).ToList();

            return list;
        }

        public List<SelectListItem> GetManufacturerList()
        {
            var list = db.Manufacturers
                  .Select(item => new SelectListItem()
                  {
                      Text = item.Name.ToString(),
                      Value = item.ManufacturerId.ToString()
                  }
                  ).ToList();

            return list;
        }

        public List<SelectListItem> GetDistributorList()
        {
            var list = db.Distributors
                  .Select(item => new SelectListItem()
                  {
                      Text = item.Name.ToString(),
                      Value = item.DistributorId.ToString()
                  }
                  ).ToList();

            return list;
        }

        public static string GetDigits(string number)
        {
            return new string(number.Where(x => char.IsDigit(x)).ToArray());
        }

        public List<SelectListItem> GetDistributorManufacturerList(int distribId, int itemId)
        {
            var item = db.Items.FirstOrDefault(t => t.ItemId == itemId);
            //   var model = new TestProgramCascadingListViewModel();
            if (item != null)
            {
                var listManufact = db.DistributorManufacturers.Where(c => c.DistributorId == distribId).Select(c => new SelectListItem()
                {
                    Text = c.Manufacturer.Name,
                    Value = c.ManufacturerId.ToString(),
                    Selected = (c.ManufacturerId == item.ManufacturerId)
                }).ToList();
                return listManufact;
            }
            else
            {
                var listManufact = db.DistributorManufacturers.Where(c => c.DistributorId == distribId).Select(c => new SelectListItem()
                {
                    Text = c.Manufacturer.Name,
                    Value = c.ManufacturerId.ToString(),

                }).ToList();
                return listManufact;
            }
        }
         
    }
}