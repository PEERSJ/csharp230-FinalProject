namespace LearningCenter.WebSite.Models
{
    public class ClassModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public ClassModel(int id, string name, string descrption, decimal price)
        {
            Id = id;
            Name = name;
            Description = descrption;
            Price = price;
        }
    }
}