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
    public class CategoryBusinessLogic : BusinessLogicBase
    { 
        public DataSourceResult GetCategory(DataSourceRequest request)
        {
            var queryResult = db.Categories
                 .ToList()
                 .OrderByDescending(s => s.CategoryId).ToDataSourceResult(request);
            return queryResult;
        }

        public Category AddCategory(DataSourceRequest request, Category model)
        {
            var category = new Category();

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                category.Name = model.Name;

                category.CreatedDate = DateTime.Now;

                db.Categories.Add(category);
                db.SaveChanges();
                dbContextTransaction.Commit();
            }

            return category;
        }

        public void UpdateCategory(DataSourceRequest request, Category model)
        {
            var category = db.Categories.Find(model.CategoryId);

            if (category != null)
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {

                    category.Name = model.Name;

                    category.ModifiedDate = DateTime.Now;

                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
            }
        }

        public void DeleteCategory(DataSourceRequest request, Category model)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                var category = db.Categories.Find(model.CategoryId);
                db.Categories.Remove(category);
                db.SaveChanges();
                dbContextTransaction.Commit();
            }
        }
    }
}