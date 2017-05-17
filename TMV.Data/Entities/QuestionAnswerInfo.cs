using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMV.Data.Entities
{
    public class QuestionAnswerInfo
    {
        public int QuestionAnswerId { get; set; }
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PublishDate { get; set; }
        public int RoleId { get; set; }
        public int AuthorId { get; set; }
        public int EditorId { get; set; }
        public int ApproverId { get; set; }
        public string AdminNote { get; set; }
        public string Question { get; set; }
        public string Slug { get; set; }

        public string NavigationUrl { get { return Navigation.NavigateQADetail(Slug); } }
    }
}
