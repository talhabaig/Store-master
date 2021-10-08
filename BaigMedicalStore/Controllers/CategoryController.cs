using BaigMedicalStore.BusinessLogic;
using BaigMedicalStore.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BaigMedicalStore.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
 
        public ActionResult CategoryRead([DataSourceRequest] DataSourceRequest request)
        {
            CategoryBusinessLogic obj = new CategoryBusinessLogic();
            DataSourceResult lstCandidateSiblings = obj.GetCategory(request);
            return Json(lstCandidateSiblings, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CategoryCreate([DataSourceRequest] DataSourceRequest request, Category model)
        {
            CategoryBusinessLogic obj = new CategoryBusinessLogic();

            try
            {
                if (model != null && ModelState.IsValid)
                {
                    var category = obj.AddCategory(request, model);

                    model.CategoryId = category.CategoryId;
                }
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                Response.StatusDescription = "Model error has been occured.";
                ModelState.AddModelError("ERROR", "Model error has been occured.");
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CategoryUpdate([DataSourceRequest] DataSourceRequest request, Category model)
        {
            CategoryBusinessLogic obj = new CategoryBusinessLogic();

            try
            {
                if (model != null && ModelState.IsValid)
                {
                    obj.UpdateCategory(request, model);
                }
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                Response.StatusDescription = "Model error has been occured.";
                ModelState.AddModelError("ERROR", "Model error has been occured.");
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CategoryDestroy([DataSourceRequest] DataSourceRequest request, Category model)
        {
            CategoryBusinessLogic obj = new CategoryBusinessLogic();

            try
            {
                obj.DeleteCategory(request, model);
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                Response.StatusDescription = "Model error has been occured.";
                ModelState.AddModelError("ERROR", "Model error has been occured.");
            }
             
            DataSourceResult lstCateg = obj.GetCategory(request);

            return Json(lstCateg, JsonRequestBehavior.AllowGet);
        }

    }
}