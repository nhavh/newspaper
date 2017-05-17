using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMV.Data.Entities
{
    public class CustomerInfo
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PublishDate { get; set; }
        public int RoleId { get; set; }
        public int AuthorId { get; set; }
        public int EditorId { get; set; }
        public int ApproverId { get; set; }
        public string AdminNote { get; set; }
    }
}
