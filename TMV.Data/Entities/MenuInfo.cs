namespace TMV.Data.Entities
{
    public class MenuInfo
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string CategoryName { get; set; }
        public string Slug { get; set; }
        public int Group { get; set; }
        public int OrderBy { get; set; }
        public string NavigationUrl => Navigation.NavigateCategory(Slug);
    }
}
