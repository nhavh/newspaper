using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMV.Data.Entities
{
    public class HyperLinkInfo
    {
        public int HyperLinkId { get; set; }
        public string OldChar { get; set; }
        public string NewChar { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
