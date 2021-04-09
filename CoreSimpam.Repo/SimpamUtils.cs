using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoreSimpam.Repo
{
    public static class SimpamUtils
    {
        /// <summary>
        /// skip modelstate to validate parameters
        /// param => param1,param2,param3
        /// </summary>

        public static void SkipToValid(this ModelStateDictionary modelState, string param)
        {
            foreach (string item in param.Split(',').ToArray())
                modelState.Remove(item);
        }
    }
    public static class StringExtension
    {

        public static string ToBase64(this string text)
        {
            return ToBase64(text, Encoding.UTF8);
        }
        public static string ToBase64(this string text, Encoding encoding)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            byte[] textAsBytes = encoding.GetBytes(text);
            return Convert.ToBase64String(textAsBytes);
        }
        public static bool TryParseBase64(this string text, out string decodedText)
        {
            return TryParseBase64(text, Encoding.UTF8, out decodedText);
        }

        public static bool TryParseBase64(this string text, Encoding encoding, out string decodedText)
        {
            if (string.IsNullOrEmpty(text))
            {
                decodedText = text;
                return false;
            }

            try
            {
                byte[] textAsBytes = Convert.FromBase64String(text);
                decodedText = encoding.GetString(textAsBytes);
                return true;
            }
            catch (Exception)
            {
                decodedText = null;
                return false;
            }
        }

        public static string FromBase64(this string base64EncodedData)
        {
            if (string.IsNullOrEmpty(base64EncodedData))
            {
                return base64EncodedData;
            }
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static bool ToBoolean(this string value)
        {
            switch (value.ToLower())
            {
                case "true":
                    return true;
                case "t":
                    return true;
                case "1":
                    return true;
                case "0":
                    return false;
                case "false":
                    return false;
                case "f":
                    return false;
                default:
                    return false;
            }
        }
        public static string ToJSON<T>(this T text)
        {
            return JsonConvert.SerializeObject(text);
        }
        public static DataTable FromJSON(this string text)
        {
            return JsonConvert.DeserializeObject<DataTable>(text);
        }
    }
    public static class PrincipalExtension
    {
        public static string GetUserRole(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.Role).Value;
        }
    }
}
