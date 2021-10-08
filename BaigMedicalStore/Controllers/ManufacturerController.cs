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
    //[Authorize]
    public class ManufacturerController : BaseController
    {
        // GET: Manufacturer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetManufacturers([DataSourceRequest] DataSourceRequest request)
        {
            ManufacturerBusinessLogic obj = new ManufacturerBusinessLogic();
            DataSourceResult lstmans = obj.GetManufacturers(request);
            return Json(lstmans, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(int manufactId = 0)
        {
            ManufacturerBusinessLogic bl = new ManufacturerBusinessLogic();
            var model = bl.GetManufacturerViewModel(manufactId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ManufacturerViewModel model)
        {
            ManufacturerBusinessLogic bl = new ManufacturerBusinessLogic();
            MessageModel messageModel = new MessageModel();

            try
            {
                if (ModelState.IsValid)
                {
                    bl.SaveManufacturer(model);
                    messageModel.Message = "Manufacturer has been saved successfully";
                }
                else
                {
                    throw new InvalidModelException();
                }
            }
            catch (InvalidModelException ex)
            {
                messageModel.Message = "Invalid information";
                messageModel.Type = Enumeration.MessageType.Error;
                logger.Error(messageModel.Message, ex);
            }
            catch (System.Exception ex)
            {
                messageModel.Message = "An error has occured while saving manufacturer";
                messageModel.Type = Enumeration.MessageType.Error;
                logger.Error(messageModel.Message, ex);
            }
            finally
            {
                TempData["MESSAGE"] = JsonConvert.SerializeObject(new { Message = messageModel.Message, Type = messageModel.Type });
            }

            if (messageModel.Type != Enumeration.MessageType.Success)
            {
                return View(model);
            }
            else
            {
                return Redirect(CommonFunction.GetBaseUrlForActions("Index", "Manufacturer"));
            }
            //            return Json(new { model, messageModel }, JsonRequestBehavior.AllowGet);
        }

        [DecryptParams]
        public ActionResult Edit(int manufactId)
        {
            ManufacturerBusinessLogic bl = new ManufacturerBusinessLogic();
            var model = bl.GetManufacturerViewModel(manufactId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ManufacturerViewModel model)
        {
            ManufacturerBusinessLogic bl = new ManufacturerBusinessLogic();
            MessageModel messageModel = new MessageModel();

            try
            {
                if (ModelState.IsValid)
                {
                    bl.SaveManufacturer(model);
                    messageModel.Message = "Manufacturer has been saved successfully";
                }
                else
                {
                    throw new InvalidModelException();
                }
            }
            catch (InvalidModelException ex)
            {
                messageModel.Message = "Invalid information";
                messageModel.Type = Enumeration.MessageType.Error;
                logger.Error(messageModel.Message, ex);
            }
            catch (System.Exception ex)
            {
                messageModel.Message = "An error has occured while saving manufacturer";
                messageModel.Type = Enumeration.MessageType.Error;
                logger.Error(messageModel.Message, ex);
            }
            finally
            {
                TempData["MESSAGE"] = JsonConvert.SerializeObject(new { Message = messageModel.Message, Type = messageModel.Type });
            }

            if (messageModel.Type != Enumeration.MessageType.Success)
            {
                return View(model);
            }
            else
            {
                return Redirect(CommonFunction.GetBaseUrlForActions("Index", "Manufacturer"));
            }

        }

        [HttpPut]
        public ActionResult ChangeManufacturerStatus(int id)
        {
            MessageModel model = new MessageModel();

            ManufacturerBusinessLogic objManufacturerBusinessLogic = new ManufacturerBusinessLogic();

            try
            {
                objManufacturerBusinessLogic.ChangeManufacturerStatus(id);
                model.Message = "Manufacturer status successfully changed";
            }
            catch (System.Exception ex)
            {
                model.Message = "An error has occured while changing manufacturer status";
                model.Type = Enumeration.MessageType.Error;
                logger.Error(model.Message, ex);
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