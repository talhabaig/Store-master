using BaigMedicalStore.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BaigMedicalStore.BusinessLogic
{
    public class LocationBusinessLogic : BusinessLogicBase
    {
        public DataSourceResult GetLocation(DataSourceRequest request)
        {
            var queryResult = db.Locations
                 .ToList()
                 .OrderByDescending(s => s.LocationId).ToDataSourceResult(request);
            return queryResult;
        }

        public Location AddLocation(DataSourceRequest request, Location model)
        {
            var location = new Location();

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                location.Name = model.Name;

                location.CreatedDate = DateTime.Now;

                db.Locations.Add(location);
                db.SaveChanges();
                dbContextTransaction.Commit();
            }

            return location;
        }

        public void UpdateLocation(DataSourceRequest request, Location model)
        {
            var location = db.Locations.Find(model.LocationId);

            if (location != null)
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {

                    location.Name = model.Name;

                    location.ModifiedDate = DateTime.Now;

                    db.Entry(location).State = EntityState.Modified;
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
            }
        }

        public void DeleteLocation(DataSourceRequest request, Location model)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                var Location = db.Locations.Find(model.LocationId);
                db.Locations.Remove(Location);
                db.SaveChanges();
                dbContextTransaction.Commit();
            }
        }

    }
}