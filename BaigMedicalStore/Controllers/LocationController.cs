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
    public class LocationController : BaseController
    {
        // GET: Location
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LocationRead([DataSourceRequest] DataSourceRequest request)
        {
            LocationBusinessLogic obj = new LocationBusinessLogic();
            DataSourceResult lstLocations = obj.GetLocation(request);
            return Json(lstLocations, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LocationCreate([DataSourceRequest] DataSourceRequest request, Location model)
        {
            LocationBusinessLogic obj = new LocationBusinessLogic();

            try
            {
                if (model != null && ModelState.IsValid)
                {
                    var Location = obj.AddLocation(request, model);

                    model.LocationId = Location.LocationId;
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

        public ActionResult LocationUpdate([DataSourceRequest] DataSourceRequest request, Location model)
        {
            LocationBusinessLogic obj = new LocationBusinessLogic();

            try
            {
                if (model != null && ModelState.IsValid)
                {
                    obj.UpdateLocation(request, model);
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

        public ActionResult LocationDestroy([DataSourceRequest] DataSourceRequest request, Location model)
        {
            LocationBusinessLogic obj = new LocationBusinessLogic();

            try
            {
                obj.DeleteLocation(request, model);
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                Response.StatusDescription = "Model error has been occured.";
                ModelState.AddModelError("ERROR", "Model error has been occured.");
            }

            DataSourceResult lstCateg = obj.GetLocation(request);

            return Json(lstCateg, JsonRequestBehavior.AllowGet);
        }
         
    }
}