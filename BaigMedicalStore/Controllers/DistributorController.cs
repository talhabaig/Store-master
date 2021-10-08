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
    public class DistributorController : BaseController
    {
        // GET: Distributor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDistributors([DataSourceRequest] DataSourceRequest request)
        {
            DistributorBusinessLogic obj = new DistributorBusinessLogic();
            DataSourceResult lstDistribs = obj.GetDistributors(request);
            return Json(lstDistribs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(int distributId = 0)
        {
            DistributorBusinessLogic bl = new DistributorBusinessLogic();
            var model = bl.GetDistributorViewModel(distributId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(DistributorViewModel model)
        {
            DistributorBusinessLogic bl = new DistributorBusinessLogic();
            MessageModel messageModel = new MessageModel();

            try
            {
                if (ModelState.IsValid)
                {
                    bl.SaveDistributor(model);
                    messageModel.Message = "Distributor has been saved successfully";
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
                messageModel.Message = "An error has occured while saving Distributor";
                messageModel.Type = Enumeration.MessageType.Error;
                logger.Error(messageModel.Message, ex);
            }

            if (messageModel.Type != Enumeration.MessageType.Success)
            {
                return View(model);
            }
            else
            {
                return Redirect(CommonFunction.GetBaseUrlForActions("Index", "Distributor"));
            }

            //return Json(new { model, messageModel }, JsonRequestBehavior.AllowGet);
        }

        [DecryptParams]
        public ActionResult Edit(int distributId)
        {
            DistributorBusinessLogic bl = new DistributorBusinessLogic();
            var model = bl.GetDistributorViewModel(distributId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DistributorViewModel model)
        {
            DistributorBusinessLogic bl = new DistributorBusinessLogic();
            MessageModel messageModel = new MessageModel();

            try
            {
                if (ModelState.IsValid)
                {
                    bl.SaveDistributor(model);
                    messageModel.Message = "Distributor has been saved successfully";
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
                messageModel.Message = "An error has occured while saving Distributor";
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
                return Redirect(CommonFunction.GetBaseUrlForActions("Index", "Distributor"));
            }
        }
         
        [HttpPut]
        public ActionResult ChangeDistributorStatus(int id)
        {
            MessageModel model = new MessageModel();

            DistributorBusinessLogic objDistributorBusinessLogic = new DistributorBusinessLogic();

            try
            {
                objDistributorBusinessLogic.ChangeDistributorStatus(id);
                model.Message = "Distributor status successfully changed";
            }
            catch (System.Exception ex)
            {
                model.Message = "An error has occured while changing Distributor status";
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