using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BaigMedicalStore.BusinessLogic;
using BaigMedicalStore.Models;

namespace BaigMedicalStore.Controllers
{
    public class InvoiceController : BaseController
    {
        BMSEntities db = new BMSEntities();
        // GET: Invoice
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PrintInvoice(int invoiceId)
        {
            InvoiceBusinessLogic obj = new InvoiceBusinessLogic();
            InvoiceViewModel model =obj.GetInvoice(invoiceId);
            return View(model); 
        }

        public JsonResult MedName(string prefix)
        {
            var mednames = db.Items.Select(product => new InvoiceModel
            {
                ItemId = product.ItemId,
                Name = product.Name,
            });

            if (!string.IsNullOrEmpty(prefix))
            {
                mednames = mednames.Where(p => p.Name.Contains(prefix));
            }

            return Json(mednames, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getData(string Name, string Code)
        {
            var mednames = db.Items.Where(x => x.Name == Name || x.Code == Code && x.IsActive == true).Select(product => new InvoiceModel
            {
                ItemId = product.ItemId,
                Name = product.Name,
                PurchasePrice = product.PurchasePrice,
                Code = product.Code,
                SalePrice = product.SalePrice,
                UnitPrice = product.UnitPrice,
                Pieceinpack = product.PiecesInPacking,
                LimitNotification = product.LimitNotification,
                TotalStock = product.TotalStock,
                Quantity = 1,
                Discount = 0
            });
            return Json(mednames, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult getInvoiceData(string InvoiceId)
        //{
        //    var mednames = db.Items.Where(x => x.Name == Name || x.Code == Code).Select(product => new InvoiceModel
        //    {
        //        ItemId = product.ItemId,
        //        Name = product.Name,
        //        PurchasePrice = product.PurchasePrice,
        //        Code = product.Code,
        //        SalePrice = product.SalePrice,
        //        UnitPrice = product.UnitPrice,
        //        Pieceinpack = product.PiecesInPacking,
        //        LimitNotification = product.LimitNotification,
        //        TotalStock = product.TotalStock,
        //        Quantity = 1,
        //        Discount = 0
        //    });
        //    return Json(mednames, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult saveData(InvoiceViewModel model)
        {
            InvoiceBusinessLogic obj = new InvoiceBusinessLogic();
            var id = obj.SaveInvoice(model);

            return Json(new
            {
                redirectUrl = Url.Action("PrintInvoice", "Invoice", new { invoiceId = id }),
                isRedirect = true
            }, JsonRequestBehavior.AllowGet);
        }

    }
}