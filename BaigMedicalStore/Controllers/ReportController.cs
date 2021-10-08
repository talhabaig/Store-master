using BaigMedicalStore.BusinessLogic;
using BaigMedicalStore.Common;
using BaigMedicalStore.Filters;
using BaigMedicalStore.Models;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaigMedicalStore.Controllers
{
    [Authorize]
    public class ReportController : BaseController
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetItems([DataSourceRequest] DataSourceRequest request)
        {
            ReportBusinessLogic obj = new ReportBusinessLogic();
            DataSourceResult lstItems = obj.GetItems(request);
            return Json(lstItems, JsonRequestBehavior.AllowGet);
        }

    }
}