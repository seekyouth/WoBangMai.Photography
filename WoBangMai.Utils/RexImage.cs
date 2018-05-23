/**
* 命名空间: WoBangMai.Utils
*
* 功 能： N/A
* 类 名： ImgRexReplase
*
* Ver 变更日期 负责人  
* ───────────────────────────────────
* V0.01 2016/10/29 10:49:51  张张  
*
* Copyright (c) 2015 Lir Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：我帮买　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WoBangMai.Utils
{
    public static class RexImage
    {

        //        ------解决方案--------------------
        //C# code
        //        string s = @"<img src=""/images/news/xxxx1.jpg"" />
        //<img src=""/images/news/xxxx2.jpg"" />
        //<img src=""/images/news/xxxx3.jpg"" />";
        //        string r = Regex.Replace(s, @"(?is)(?<=<img[^>]+src="").+?(?=""[^>]*/>)", "http://www.xxx.com$0");
        //        Response.Write(Server.HtmlEncode(r));

        //------解决方案--------------------
        //Regex imgsrc = new Regex(@"(?<=src=")(?=/)", RegexOptions.None)

        //Regex.Replace(reContent, regImg.ToString(), "http://www.xxx.com",RegexOptions.Compiled | RegexOptions.IgnoreCase);
        //------解决方案--------------------
        //string s = @"<IMG src=""/images/news/xxxx1.jpg"">";
        //        s = Regex.Replace(s, @"(?is)(?<=<IMG\ssrc="").*?[^>](?=/)", "http://www.xxx.com/images");
        //Console.WriteLine(s);

        public static string ReplaseImgSrc(string html, string baseUrl)
        {
            return Regex.Replace(html, @"(?is)(?<=<img[^>]+src="").+?(?=""[^>]*/>)", baseUrl + "$0");
        }
    }
}
