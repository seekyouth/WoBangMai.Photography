
using Codeplex.Data;
using Newtonsoft.Json;
/**
* 命名空间: WoBangMai.SDK.CMS
*
* 功 能： N/A
* 类 名： NewsAPI
*
* Ver 变更日期 负责人  
* ───────────────────────────────────
* V0.01 2018/3/14 23:14:39  张张  
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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WoBangMai.CMS.Entity;
using WoBangMai.Models;
using WoBangMai.SDK.AutoMapper;

namespace WoBangMai.SDK.CMS
{
  public  class NewsAPI
    {
        public static string baseUrl = "";
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static News Get(string token, string content)
        {
            var client = new HttpClient();
            var result = client.PostAsync(string.Format("{0}/ArticleNews/Get?token={1}", baseUrl, token), new StringContent(content)).Result;
            return AutoMapperHelper.MapTo<News>(JsonConvert.DeserializeObject<dt_article_news>(result.Content.ReadAsStringAsync().Result));
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static List<News> GetList(string token,string content)
        {
            var client = new HttpClient();
            var result = client.PostAsync(string.Format("/ArticleNews/GetList?token={0}&id={1}",baseUrl, token), new StringContent(content)).Result;
            return AutoMapperHelper.MapTo<List<News>>(JsonConvert.DeserializeObject<List<dt_article_news>>(result.Content.ReadAsStringAsync().Result));
        }

    }
}
