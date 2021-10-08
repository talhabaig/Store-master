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
    public class ItemController : BaseController
    {
        // GET: Item
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetItems([DataSourceRequest] DataSourceRequest request)
        {
            ItemBusinessLogic obj = new ItemBusinessLogic();
            DataSourceResult lstItems = obj.GetItems(request);
            return Json(lstItems, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(int itemId = 0)
        {
            ItemBusinessLogic bl = new ItemBusinessLogic();
            var model = bl.GetItemViewModel(itemId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ItemViewModel model)
        {
            ItemBusinessLogic bl = new ItemBusinessLogic();
            MessageModel messageModel = new MessageModel();

            try
            {
                if (ModelState.IsValid)
                {
                    bl.SaveItem(model);
                    messageModel.Message = "Item has been saved successfully";
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
                messageModel.Message = "An error has occured while saving Item";
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
                return Redirect(CommonFunction.GetBaseUrlForActions("Index", "Item"));
            }
        }

        [DecryptParams]
        public ActionResult Edit(int itemId)
        {
            ItemBusinessLogic bl = new ItemBusinessLogic();
            var model = bl.GetItemViewModel(itemId);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ItemViewModel model)
        {
            ItemBusinessLogic bl = new ItemBusinessLogic();
            MessageModel messageModel = new MessageModel();

            try
            {
                if (ModelState.IsValid)
                {
                    bl.SaveItem(model);
                    messageModel.Message = "Item has been saved successfully";
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
                messageModel.Message = "An error has occured while saving Item";
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
                return Redirect(CommonFunction.GetBaseUrlForActions("Index", "Item"));
            }
        }

        [HttpPut]
        public ActionResult ChangeItemStatus(int id)
        {
            MessageModel model = new MessageModel();

            ItemBusinessLogic objItemBusinessLogic = new ItemBusinessLogic();

            try
            {
                objItemBusinessLogic.ChangeItemStatus(id);
                model.Message = "Item status successfully changed";
            }
            catch (System.Exception ex)
            {
                model.Message = "An error has occured while changing Item status";
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

        [HttpPost]
        public JsonResult IsItemNameUnique(string Name)
        {
            var obj = new ItemBusinessLogic();
            var result = obj.IsItemnameUnique(Name);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}