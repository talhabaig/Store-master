using BaigMedicalStore.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaigMedicalStore.Controllers
{
    public class CommonController : BaseController
    {
        // GET: Common
        public ActionResult GetCategoryList()
        {
            CommonBusinessLogic objCommonBusinessLogic = new CommonBusinessLogic();
            var list = objCommonBusinessLogic.GetCategoryList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLocationList()
        {
            CommonBusinessLogic objCommonBusinessLogic = new CommonBusinessLogic();
            var list = objCommonBusinessLogic.GetLocationList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetManufacturerList()
        {
            CommonBusinessLogic objCommonBusinessLogic = new CommonBusinessLogic();
            var list = objCommonBusinessLogic.GetManufacturerList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDistributorList()
        {
            CommonBusinessLogic objCommonBusinessLogic = new CommonBusinessLogic();
            var list = objCommonBusinessLogic.GetDistributorList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDistributorManufacturerList(int distribId, int itemId = 0)
        {
            CommonBusinessLogic objCommonBusinessLogic = new CommonBusinessLogic();

            var model = objCommonBusinessLogic.GetDistributorManufacturerList(distribId, itemId);

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}