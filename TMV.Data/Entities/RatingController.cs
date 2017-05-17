using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class RatingController
    {
        public RatingInfo InsertRating(RatingInfo info)
        {
            return CBO.FillObject<RatingInfo>(SQL.InsertRating(info.UserId, info.ItemId, info.ItemType, info.Price, info.Quality, info.Doctor, info.Attitude, info.Facility, info.Customer));
        }
    }
}
