using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoBangMai.Models
{
    public partial class cms_category : Entity
    {

        public int Category_ID
        { get; set; }

        public string CategoryName
        { get; set; }

        public string CategoryKeyword
        { get; set; }

        public string IconClass
        { get; set; }

        public int ParentID
        { get; set; }

        public bool? IsVisible
        { get; set; }

        public bool? IsEnable
        { get; set; }

        public string CategoryRemark
        { get; set; }

        public int CategorySeq
        { get; set; }


        public List<cms_category> ChildList { get; set; }

        public cms_category Prent { get; set; }
    }
}
