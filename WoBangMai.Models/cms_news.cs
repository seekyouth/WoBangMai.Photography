using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoBangMai.Models
{
    public partial class cms_news : Entity
    {
        public int News_ID
        { get; set; }

        public string NewsTitle
        { get; set; }

        public string NewsAuthor
        { get; set; }

        public string NewsFrom
        { get; set; }

        public string ShortContent
        { get; set; }

        public string NewsContent
        { get; set; }

        public string NewsPic
        { get; set; }

        public int NewsCategoryID
        { get; set; }

        public bool? IsFirst
        { get; set; }

        public bool? IsHot
        { get; set; }

        public bool? IsRec
        { get; set; }

        public bool? IsTuWen
        { get; set; }

        public DateTime? CreateTime
        { get; set; }

        public DateTime? LastUpdateTime
        { get; set; }

        public int NewsPV
        { get; set; }

    }
}
