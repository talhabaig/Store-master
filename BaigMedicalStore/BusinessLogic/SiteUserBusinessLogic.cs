using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.IO;
using System.Web;
using System.Collections;
using BaigMedicalStore.Models;

namespace BaigMedicalStore.BusinessLogic
{
    public class SiteUserBusinessLogic : BusinessLogicBase
    {
        public static string GetLoggedInUserFirstName(string userId)
        {
            var firstName = string.Empty;
            using (var db = new BMSEntities())
            {
                var user = db.Users.FirstOrDefault(u => u.AspNetUserId == userId);
                if (user != null)
                {
                    firstName = user.FullName;
                }
            }

            return firstName;
        }
         
        public long GetLoggedInUserId(long userId)
        {
            long _userId = 0;
            using (var db = new BMSEntities())
            {
                var user = db.Users.FirstOrDefault(u => u.UserId == userId);
                if (user != null)
                {
                    _userId = user.UserId;
                }
            }

            return _userId;
        }


    }
}
