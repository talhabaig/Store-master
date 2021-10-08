using BaigMedicalStore.Common;
using BaigMedicalStore.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace BaigMedicalStore.BusinessLogic
{
    public class ItemBusinessLogic : BusinessLogicBase
    {
        public ItemViewModel GetItemViewModel(int itemId, ItemViewModel viewModel = null)
        {
            viewModel = viewModel ?? new ItemViewModel();

            var item = db.Items.FirstOrDefault(c => c.ItemId == itemId);
            if (item != null)
            {
                Transform.FromObjectToObject(item, viewModel);

                viewModel.LocationName = (item.LocationId == null || item.LocationId == 0) ? "" : db.Locations.FirstOrDefault(c => c.LocationId == item.LocationId).Name;
          
                viewModel.PiecesInPaking = item.PiecesInPacking;
            }

            return viewModel;
        }

        public DataSourceResult GetItems(DataSourceRequest request)
        {
            Hashtable fltr = new Hashtable();
            Common.CommonFunction.PopulateFiltersInHashTable(request.Filters, fltr);

            string sortBy = string.Empty;
            if (request.Sorts.Any())
                sortBy = request.Sorts[0].Member + " " + request.Sorts[0].SortDirection;

            ObjectParameter objparam = new ObjectParameter("TotalRecords", System.Data.DbType.Int16);

            var name = fltr.ContainsKey("Name") ? fltr["Name"].ToString() : null;
            var code = fltr.ContainsKey("Code") ? fltr["Code"].ToString() : null;
             var locat = fltr.ContainsKey("Location") ? fltr["Location"].ToString() : null; 

            var queryResult = db.Item_Get(name, code,locat, request.Page, request.PageSize, sortBy, objparam).ToList()
                .Select(i => new ItemViewModel()
                {
                     Name = i.Name,
                      Code = i.Code ?? "",
                    Formula = i.Formula ?? "",
                    ItemId = i.ItemId,
                    imgbarCode = i.barCodeImage,
                    LocationId = i.LocationId ?? 0,
                    Status = (i.IsActive) ? "Active" : "Inactive",
                    LocationName = i.Location ?? "",
                    PiecesInPaking = i.PiecesInPacking ?? 0,
                    PurchasePrice = i.PurchasePrice,
                    SalePrice = i.SalePrice ?? 0,
                    LimitNotification = i.LimitNotification ?? 0,
                    UnitPrice = i.UnitPrice ?? 0,
                    TotalStock = (int)i.TotalStock,
                    IsActive = i.IsActive,
                    EditItemUrl = CryptographyUtility.GetEncryptedQueryString(new { itemId = i.ItemId })
                });

            DataSourceResult dsr = new DataSourceResult();
            dsr.Data = queryResult;
            dsr.Total = (int)objparam.Value;

            return dsr;
        }

        public void SaveItem(ItemViewModel model)
        {
            var item = new Item();
            if (model.ItemId > 0)
            {
                item = db.Items.FirstOrDefault(c => c.ItemId == model.ItemId);
            }
            if (model.Code.Length > 0)
            {
                var barcodeImage = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum.Draw(model.Code, 50);
                var resultImage = new Bitmap(model.Code.Length * 40, 150); // 20 is bottom padding, to adjust text.
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (Graphics graphics = Graphics.FromImage(resultImage))
                    {
                        Font font = new Font("IDAutomationHC39M", 20);
                        SolidBrush brush = new SolidBrush(Color.Black);
                        PointF point = new PointF(25f, 50f);
                        graphics.Clear(Color.White);
                        graphics.DrawImage(barcodeImage, 0, 0);
                        graphics.DrawString(model.Code, font, brush, point);
                    }
                    resultImage.Save(memoryStream, ImageFormat.Png);
                    item.barCodeImage = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                }

            }
            item.Name = model.Name;             
            item.LocationId = model.LocationId; 
            item.Formula = model.Formula;
            item.Code = model.Code;
            item.IsActive = true;
            item.PiecesInPacking = model.PiecesInPaking;
            item.PurchasePrice = model.PurchasePrice;   
            item.LimitNotification = model.LimitNotification;
            item.SalePrice = model.SalePrice;
            item.UnitPrice = model.SalePrice / model.PiecesInPaking;
            item.TotalStock = model.TotalStock;
            if (item.ItemId == 0)
            {
                item.CreatedBy = (int)GetLoggedInUserId();
                item.CreatedOn = DateTime.UtcNow;

                db.Items.Add(item);
            }
            else
            {
                item.UpdatedBy = (int)GetLoggedInUserId();
                item.UpdatedOn = DateTime.UtcNow;

                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
        }
 
        public void ChangeItemStatus(int itemId)
        {
            ToggleActiveStatus<Item>(sub => sub.ItemId == itemId);
        }

        public bool IsItemnameUnique(string name)
        {
            var IsUnique = !db.Items.Any(c => c.Name.Trim().ToLower() == name.Trim().ToLower());
            return IsUnique;
        }
    }
}