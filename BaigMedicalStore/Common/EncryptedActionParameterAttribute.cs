using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BaigMedicalStore.Common;

namespace BaigMedicalStore.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DecryptParamsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                Dictionary<string, object> decryptedParameters = new Dictionary<string, object>();

                if (HttpContext.Current.Request.QueryString.Get("q") != null)
                {
                    string encryptedQueryString = HttpContext.Current.Request.QueryString.Get("q").Replace(" ", "+");
                    string decrptedString = CryptographyUtility.Decrypt(encryptedQueryString.ToString());
                    string[] paramsArrs = decrptedString.Split('?');

                    for (int i = 0; i < paramsArrs.Length; i++)
                    {
                        string[] paramArr = paramsArrs[i].Split('=');

                        if (paramArr.Length == 2)
                        {
                            paramArr[0] = paramArr[0].Trim();
                            paramArr[1] = paramArr[1].Trim();
                            var isNumeric = !string.IsNullOrEmpty(paramArr[1]) && paramArr[1].All(Char.IsDigit);
                            var isbool = paramArr[1].ToLower() == "true" ? true : paramArr[1].ToLower() == "false" ? true : false;

                            if (isNumeric)
                            {
                                decryptedParameters.Add(paramArr[0], Convert.ToInt32(paramArr[1]));
                            }
                            else if (isbool)
                            {
                                decryptedParameters.Add(paramArr[0], Convert.ToBoolean(paramArr[1]));
                            }
                            else
                            {
                                decryptedParameters.Add(paramArr[0], paramArr[1]);
                            }
                        }
                    }
                }

                foreach (var param in decryptedParameters)
                {
                    filterContext.ActionParameters[param.Key] = param.Value;
                }

                //for (int i = 0; i < decryptedParameters.Count; i++)
                //{
                //    filterContext.ActionParameters[decryptedParameters.Keys.ElementAt(i)] = decryptedParameters.Values.ElementAt(i);
                //}

                base.OnActionExecuting(filterContext);
            }
            catch (System.Exception ex)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
            }
        }

    }
}