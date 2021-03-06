﻿
using Codeplex.Data;
/**
* 命名空间: WoBangMai.SDK
*
* 功 能： N/A
* 类 名： BaseAPI
*
* Ver 变更日期 负责人  
* ───────────────────────────────────
* V0.01 2018/3/14 23:17:47  张张  
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

namespace WoBangMai.SDK
{
   public class BaseAPI
    {
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static dynamic GetWebConfig(string token, string content)
        {
            var client = new HttpClient();
            var result = client.PostAsync(string.Format("/WebConfig/Get?token={0}", token), new StringContent(content)).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
    }
}
