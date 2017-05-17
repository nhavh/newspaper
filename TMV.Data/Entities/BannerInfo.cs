using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMV.Data.Entities
{
    public class BannerInfo
    {
        public int BannerId { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public byte Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsMobile { get; set; }
        public string Content { get; set; }
        public int Type { get; set; }
        public string Slug { get; set; }
        public string Url { get; set; }
        public int CategoryId { get; set; }
        public int Position { get; set; }

        public string NavigationUrl
        {
            get { return Navigation.NavigatePriceDetail(Slug); }
        }
    }
}
