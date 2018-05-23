using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace WoBangMai.Utils
{
    public class CookiesHelper
    {


        /// <summary> 
        /// 获得Cookie 
        /// </summary>
        /// <param name="cookieName"></param> 
        /// <returns></returns>
        public static string GetCookieName(string cookieName)
        {
            Encoding encr =Encoding.UTF32;
            HttpRequest request = HttpContext.Current.Request;
            if (request != null && request.Cookies[cookieName] != null)
            {
                return HttpUtility.UrlDecode(request.Cookies[cookieName].Value, encr);
            }
            return "";
        }
        
        /// <summary> 
        /// 获得Cookie 
        /// </summary>
        /// <param name="cookieName"></param> 
        /// <returns></returns>
        public static string  GetCookie(string cookieName)
        {
            Encoding encr = Encoding.UTF32;
            HttpRequest request = HttpContext.Current.Request;
            HttpResponse response = HttpContext.Current.Response;
            if (request != null && request.Cookies[cookieName] != null )
            {
                return HttpUtility.UrlDecode(request.Cookies[cookieName].Value,encr); 
            }
            return null;
        }


        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        public static void ClearCookie(string cookiename)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary> 
        /// 添加Cookie 
        /// </summary>
        /// <param name="cookie"></param> 
        public static void AddCookie(HttpCookie cookie)
        {
            HttpResponse response = HttpContext.Current.Response;
            if (response != null)
            {
                //指定客户端脚本是否可以访问[默认为false] 
                cookie.HttpOnly = true;
                //指定统一的Path，比便能通存通取 
                cookie.Path = "/";
                //设置跨域,这样在其它二级域名下就都可以访问到了 //
                cookie.Domain = "chinesecoo.com"; response.AppendCookie(cookie);
            }
        }


        /// <summary> /// 设置Cookie子键的值 
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="key"></param> 
        /// <param name="value"></param> 
        public static void SetCookie(string cookieName, string key, string value)
        {
            SetCookie(cookieName, key, value, null);
        }

        /// <summary> /// 设置Cookie 
        /// </summary> 
        /// <param name="cookieName"></param>
        /// <param name="key"></param> 
        /// <param name="value"></param> 
        /// <param name="expires"></param>
        public static void SetCookie(string cookieName, string key, string value, DateTime? expires)
        {
            Encoding encr = Encoding.UTF32;
            HttpResponse response = HttpContext.Current.Response;
            value = HttpUtility.UrlEncode(value, encr);
            key = HttpUtility.UrlEncode(key, encr);
            if (response != null)
            {
                HttpCookie cookie =response.Cookies[cookieName];
                if (cookie != null)
                {
                    if (!string.IsNullOrEmpty(key) && cookie.HasKeys)
                        cookie.Values.Set(key, value);
                    else if (!string.IsNullOrEmpty(value))
                        cookie.Value = value;
                    if (expires != null )
                    { 
                       cookie.Expires = expires.Value;
                    }
                    response.SetCookie(cookie);
                }
            }
        }
    }
}

