
using AutoMapper;
using AutoMapper.Configuration;
/**
* 命名空间: WoBangMai.SDK
*
* 功 能： N/A
* 类 名： Mapping
*
* Ver 变更日期 负责人  
* ───────────────────────────────────
* V0.01 2018/5/9 22:57:14  张张  
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
using System.Threading.Tasks;
using WoBangMai.CMS.Entity;
using WoBangMai.Models;

namespace WoBangMai.SDK
{
    public class Mapping
    {
        /// <summary>
        /// 注册映射关系 
        /// </summary>
        public static void Register()
        {

            var config = new MapperConfigurationExpression();
            config.CreateMap<dt_article_news, News>()
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.id))
                .ForMember(dest => dest.SiteId, mo => mo.MapFrom(src => src.site_id))
                .ForMember(dest => dest.ChannelId, mo => mo.MapFrom(src => src.channel_id))
                .ForMember(dest => dest.CategoryId, mo => mo.MapFrom(src => src.category_id))
                .ForMember(dest => dest.Brand_id, mo => mo.MapFrom(src => src.brand_id))
                .ForMember(dest => dest.CallIndex, mo => mo.MapFrom(src => src.call_index))
                .ForMember(dest => dest.LinkUrl, mo => mo.MapFrom(src => src.link_url))
                .ForMember(dest => dest.ImgUrl, mo => mo.MapFrom(src => src.img_url))
                .ForMember(dest => dest.SeoTitle, mo => mo.MapFrom(src => src.seo_title))
                .ForMember(dest => dest.SeoKeywords, mo => mo.MapFrom(src => src.seo_keywords))
                .ForMember(dest => dest.SeoDescription, mo => mo.MapFrom(src => src.seo_description))
                .ForMember(dest => dest.Tags, mo => mo.MapFrom(src => src.tags))
                .ForMember(dest => dest.ZhaiYao, mo => mo.MapFrom(src => src.zhaiyao))
                .ForMember(dest => dest.Content, mo => mo.MapFrom(src => src.content))
                .ForMember(dest => dest.SortId, mo => mo.MapFrom(src => src.sort_id))
                .ForMember(dest => dest.Click, mo => mo.MapFrom(src => src.click))
                .ForMember(dest => dest.Status, mo => mo.MapFrom(src => src.status))
                .ForMember(dest => dest.IsMsg, mo => mo.MapFrom(src => src.is_msg))
                .ForMember(dest => dest.iSTop, mo => mo.MapFrom(src => src.is_top))
                .ForMember(dest => dest.IsRed, mo => mo.MapFrom(src => src.is_red))
                .ForMember(dest => dest.IsHot, mo => mo.MapFrom(src => src.is_hot))
                .ForMember(dest => dest.IsSlide, mo => mo.MapFrom(src => src.is_slide))
                .ForMember(dest => dest.IsSys, mo => mo.MapFrom(src => src.is_sys))
                .ForMember(dest => dest.UserName, mo => mo.MapFrom(src => src.user_name))
                .ForMember(dest => dest.LikeCount, mo => mo.MapFrom(src => src.like_count))
                .ForMember(dest => dest.AddTime, mo => mo.MapFrom(src => src.add_time))
                .ForMember(dest => dest.UpdateTime, mo => mo.MapFrom(src => src.update_time))
                .ForMember(dest => dest.Source, mo => mo.MapFrom(src => src.source))
                .ForMember(dest => dest.Author, mo => mo.MapFrom(src => src.author));

            config.CreateMap<dt_article_category, Category>()
                .ForMember(dest => dest.Id, mo => mo.MapFrom(src => src.id))
                .ForMember(dest => dest.CallIndex, mo => mo.MapFrom(src => src.call_index))
                .ForMember(dest => dest.ChannelId, mo => mo.MapFrom(src => src.channel_id))
                .ForMember(dest => dest.Content, mo => mo.MapFrom(src => src.content))
                .ForMember(dest => dest.ImgUrl, mo => mo.MapFrom(src => src.img_url))
                .ForMember(dest => dest.LinkUrl, mo => mo.MapFrom(src => src.link_url))
                .ForMember(dest => dest.ParentId, mo => mo.MapFrom(src => src.parent_id))
                .ForMember(dest => dest.SeoDescription, mo => mo.MapFrom(src => src.seo_description))
                .ForMember(dest => dest.SeoKeywords, mo => mo.MapFrom(src => src.seo_keywords))
                .ForMember(dest => dest.SeoTitle, mo => mo.MapFrom(src => src.seo_title))
                .ForMember(dest => dest.SortId, mo => mo.MapFrom(src => src.sort_id))
                .ForMember(dest => dest.Title, mo => mo.MapFrom(src => src.title));

                ////或者Mapper.Initialize(cfg => cfg.CreateMap<User, UserDTO>());
                //config.CreateMap<StudentDto, Student>()
                //  .ForMember(dest => dest.Gender, mo => mo.Ignore())
                //  .ForMember(dest => dest.Class, mo => mo.Ignore());

            Mapper.Initialize(config);
        }
    }
}
