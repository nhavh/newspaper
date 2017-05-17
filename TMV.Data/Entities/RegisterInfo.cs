using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMV.Data.Entities
{
    public class RegisterInfo
    {
        public int RegisterId { get; set; }
        public bool Gender { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public string KhoaHoc { get; set; }
        public DateTime NgayHoc { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }
    }
}
