using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMV.Data.Entities
{
    public class RatingInfo
    {
        public int RatingId { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public int ItemType { get; set; }
        public int Price { get; set; }
        public int Quality { get; set; }
        public int Doctor { get; set; }
        public int Attitude { get; set; }
        public int Facility { get; set; }
        public int Customer { get; set; }
        public float AveragePrice { get; set; }
        public float AverageQuality { get; set; }
        public float AverageDoctor { get; set; }
        public float AverageAttitude { get; set; }
        public float AverageFacility { get; set; }
        public float AverageCustomer { get; set; }
        public float TotalRating { get; set; }
        public int TotalVote { get; set; }
    }
}
