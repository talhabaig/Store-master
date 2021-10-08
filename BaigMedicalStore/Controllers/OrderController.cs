using BaigMedicalStore.BusinessLogic;
using BaigMedicalStore.Common;
using BaigMedicalStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaigMedicalStore.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AddItemToOrder(int id)
        {
            MessageModel model = new MessageModel();

            OrderBusinesslogic objOrderBusinessLogic = new OrderBusinesslogic();

            try
            {
                objOrderBusinessLogic.AddItemToOrder(id);
                model.Message = "order status successfully changed";
            }
            catch (System.Exception ex)
            {
                model.Message = "An error has occured while changing Item status";
                model.Type = Enumeration.MessageType.Error;
               // logger.Error(model.Message, ex);
            }

            var response = new
            {
                //Status = updatedStatus,
                MessageModel = model
            };

            return Json(response, JsonRequestBehavior.AllowGet);

        }

    }
}
