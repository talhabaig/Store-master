using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Routing;

namespace BaigMedicalStore.Common
{
    public class CryptographyUtility
    {
        public static string GetEncryptedQueryString(object routeValues)
        {
            string queryString = string.Empty;

            //My Changes
            bool IsRoute = false;
            if (routeValues != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(routeValues);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    //My Changes
                    if (!d.Keys.Contains("IsRoute"))
                    {
                        if (i > 0)
                        {
                            queryString += "?";
                        }
                        queryString += d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                    }
                    else
                    {
                        //My Changes
                        if (!d.Keys.ElementAt(i).Contains("IsRoute"))
                        {
                            queryString += d.Values.ElementAt(i);
                            IsRoute = true;
                        }
                    }
                }
            }
            StringBuilder hrefValue = new StringBuilder();

            if (IsRoute == false)
                hrefValue.Append("?q=" + Encrypt(queryString));
            else
                hrefValue.Append("/" + Encrypt(queryString));

            return hrefValue.ToString();

        }

        public static Dictionary<string, object> DecryptQueryString(string querystring)
        {
            Dictionary<string, object> decryptedParameters = new Dictionary<string, object>();
            try
            {

                if (querystring != null)
                {
                    string encryptedQueryString = querystring;
                    string decrptedString = Decrypt(encryptedQueryString.ToString());
                    string[] paramsArrs = decrptedString.Split('?');

                    for (int i = 0; i < paramsArrs.Length; i++)
                    {
                        string[] paramArr = paramsArrs[i].Split('=');

                        if (paramArr.Length == 2)
                        {
                            paramArr[0] = paramArr[0].Trim();
                            paramArr[1] = paramArr[1].Trim();
                            var isNumeric = !string.IsNullOrEmpty(paramArr[1]) && paramArr[1].All(Char.IsDigit);
                            var isbool = (paramArr[1].ToLower() == "true" || paramArr[1].ToLower() == "false") ? true : false;

                            if (isNumeric)
                            {
                                decryptedParameters.Add(paramArr[0], Convert.ToInt64(paramArr[1]));
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

            }
            catch (System.Exception ex)
            {
                //RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "AccessDenied" }));
            }

            return decryptedParameters;
        }

        public static string Encrypt(string plainText)
        {
            string key = AppConstants.Constant.EncryptionDecryptionKey;
            byte[] EncryptKey = { };
            byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            EncryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByte = Encoding.UTF8.GetBytes(plainText);
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(EncryptKey, IV), CryptoStreamMode.Write);
            cStream.Write(inputByte, 0, inputByte.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }
        public static string Decrypt(string encryptedText)
        {
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            MemoryStream ms = new MemoryStream();

            string key = AppConstants.Constant.EncryptionDecryptionKey;
            byte[] DecryptKey = { };
            byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            byte[] inputByte = new byte[encryptedText.Length];

            DecryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByte = Convert.FromBase64String(CorrectData(encryptedText));
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(DecryptKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByte, 0, inputByte.Length);
            cs.FlushFinalBlock();

            return encoding.GetString(ms.ToArray());
        }

        public static string CorrectData(string data)
        {
            string _data = data.Trim().Replace(" ", "+");

            if (_data.Length % 4 > 0)
            {
                _data = _data.PadRight(_data.Length + 4 - _data.Length % 4, '=');
            }

            return _data;
        }
    }
}