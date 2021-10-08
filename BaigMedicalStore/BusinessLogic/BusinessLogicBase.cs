using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Reflection;
using BaigMedicalStore.Models;
using log4net;

namespace BaigMedicalStore.BusinessLogic
{
    public class BusinessLogicBase : IDisposable
    {
        protected static readonly ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected BMSEntities db = new BMSEntities();

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                db = null;
            }
        }

        public long GetLoggedInUserId()
        {
            long userId = 0;
            var claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                // the principal identity is a claims identity.
                // now we need to find the NameIdentifier claim
                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    long.TryParse(userIdClaim.Value, out userId);
                    //long.TryParse(db.SiteUsers.ToList().Find(u => u.UserId == Convert.ToInt64(userIdClaim.Value)).UserId.ToString(), out userId);
                }
            }

            return userId;
        }

        public void ToggleActiveStatus<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            T objEntity = null;
            objEntity = db.Set<T>().FirstOrDefault(predicate);

            var statusProp = objEntity.GetType().GetProperty("IsActive", BindingFlags.Public | BindingFlags.Instance);
            var modifiedDateProp = objEntity.GetType().GetProperty("UpdatedOn", BindingFlags.Public | BindingFlags.Instance);
            var modifiedByProp = objEntity.GetType().GetProperty("UpdatedBy", BindingFlags.Public | BindingFlags.Instance);

            bool currentStatus;
            bool.TryParse(statusProp.GetValue(objEntity).ToString(), out currentStatus);
            bool newStatus = (currentStatus) ? false : true;

            statusProp.SetValue(objEntity, newStatus, null);
            modifiedDateProp?.SetValue(objEntity, DateTime.UtcNow, null);
            modifiedByProp?.SetValue(objEntity, (int)GetLoggedInUserId(), null);

            db.Set<T>().Attach(objEntity);

            var dbEntityEntry = db.Entry(objEntity);
            dbEntityEntry.Property("IsActive").IsModified = true;

            if (modifiedDateProp != null)
            {
                dbEntityEntry.Property("UpdatedOn").IsModified = true;
            }

            if (modifiedByProp != null)
            {
                dbEntityEntry.Property("UpdatedBy").IsModified = true;
            }

            db.SaveChanges();


        }

    }
}
