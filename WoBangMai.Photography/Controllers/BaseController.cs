using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WoBangMai.Photography.Controllers
{
    public class BaseController : Controller
    {

        /// <summary>
        /// 外部Url
        /// </summary>
        public static string DataSupplierUrl
        {
            get{ return ConfigurationManager.AppSettings["DataSupplierUrl"].ToString();}
        }


    }
}