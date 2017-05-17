using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMV.Data.Entities
{
    public class PictureInfo
    {
        public int PictureId { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public bool IsHome { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Slug { get; set; }

        public string UrlPath { get; set; }
        public int Total { get; set; }
        public string NavigationUrl { get { return Navigation.NavigatePictureDetail(Slug); } }
    }
}
