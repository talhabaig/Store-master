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
using Kendo.Mvc.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq;
using System.Web;

namespace BaigMedicalStore.BusinessLogic
{
    public class ReportBusinessLogic : BusinessLogicBase
    {
        public DataSourceResult GetItems(DataSourceRequest request)
        {
            Hashtable fltr = new Hashtable();
            Common.CommonFunction.PopulateFiltersInHashTable(request.Filters, fltr);

            string sortBy = string.Empty;
            if (request.Sorts.Any())
                sortBy = request.Sorts[0].Member + " " + request.Sorts[0].SortDirection;

            ObjectParameter objparam = new ObjectParameter("TotalRecords", System.Data.DbType.Int16);
            DateTime? fromDate = fltr.ContainsKey("FromDate") ? (DateTime?)fltr["FromDate"] : null;
            DateTime? toDate = fltr.ContainsKey("ToDate") ? (DateTime?)fltr["ToDate"] : null;
            var result = (from id in db.InvoiceDetails
                          join i in db.Items on id.ItemId equals i.ItemId
                          join inv in db.Invoices on id.InvoiceId equals inv.InvoiceId
                          where inv.AddedOn >= fromDate && inv.AddedOn <= toDate
                          group i by i.ItemId into g
                          select new
                          {
                              P = g.Distinct().Select(x => new ReportViewModel()
                              {
                                  ItemId = x.ItemId,
                                  Name = x.Name,
                                  PurchasePrice = (decimal)(x.PurchasePrice / x.PiecesInPacking) * (x.InvoiceDetails.Select(z => z.Quantity).FirstOrDefault()),
                                  SalePrice = x.InvoiceDetails.Select(y => y.TotalPrice).Sum(),
                                  Quantity = x.InvoiceDetails.Select(y => y.Quantity).Sum(),
                                  Profit = x.InvoiceDetails.Select(y => y.TotalPrice).Sum() - ((x.PurchasePrice / x.PiecesInPacking) * x.InvoiceDetails.Select(y => y.Quantity).Sum())
                              })
                          }
                  ).ToList();



            var queryResult = db.ReportItem_Get(fromDate, toDate, sortBy, objparam).ToList()
                .Select(i => new ReportViewModel()
                {
                    ItemId = i.ItemId,
                    Name = i.Name,
                    PurchasePrice = (decimal)i.totalPurchase,
                    SalePrice = i.TotalSale,
                    Quantity = (int)i.Quantity,
                    Profit = i.Profit
                });

            DataSourceResult dsr = new DataSourceResult();
            dsr.Data = queryResult;
            dsr.Total = (int)objparam.Value;

            return dsr;
        }

    }
}