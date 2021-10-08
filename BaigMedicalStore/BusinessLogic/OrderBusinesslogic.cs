using BaigMedicalStore.Common;
using BaigMedicalStore.Models;
using BaigMedicalStore.ViewModel;
using Kendo.Mvc.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace BaigMedicalStore.BusinessLogic
{
    public class OrderBusinesslogic : BusinessLogicBase
    {
        public OrderViewModel GetOrderViewModel(int OrderId, OrderViewModel viewModel = null)
        {
            viewModel = viewModel ?? new OrderViewModel();

            var order = db.Orders.FirstOrDefault(c => c.OrderId == OrderId);
            if (order != null)
            {
                Transform.FromObjectToObject(order, viewModel);

               
                viewModel.DistributorName = order.Distributor?.Name;
          
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

           
            var distribut = fltr.ContainsKey("Distributor") ? fltr["Distributor"].ToString() : null;
          


            //var queryResult = db.Order_Get( distribut, request.Page, request.PageSize, sortBy, objparam).ToList()
            //    .Select(i => new OrderViewModel()
            //    {
                   
            //        DistributorId = i.DistributorId ,
            //        DistributorName = i.Distributor ?? "",

            //        IsDispatched = i.IsDispatched,
            //        EditItemUrl = CryptographyUtility.GetEncryptedQueryString(new { OrderId = i.OrderId })
            //    });

            DataSourceResult dsr = new DataSourceResult();
            dsr.Data = null;
            dsr.Total = (int)objparam.Value;

            return dsr;
        }

        public void SaveItem(OrderViewModel model)
        {
            var order = new Order();
            if (model.OrderId > 0)
            {
                order = db.Orders.FirstOrDefault(c => c.OrderId == model.OrderId);
            }


            order.DistributorId = model.DistributorId;

            order.IsDispatched = false;
            
            if (order.OrderId == 0)
            {
                order.CreatedBy = (int)GetLoggedInUserId();
                order.CreatedOn = DateTime.UtcNow;
                //db.Items.Add(order);
            }
            else
            {
                order.CreatedBy = (int)GetLoggedInUserId();
                order.CreatedOn = DateTime.UtcNow;

                db.Entry(order).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
        }

        public void AddItemToOrder(int itemId)
        {
            ToggleActiveStatus<Order>(sub => sub.OrderId == itemId);
        }
    }
}