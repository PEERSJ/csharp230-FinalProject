namespace LearningCenter.WebSite.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ClassModel[] Classes { get; set; }
    }
}