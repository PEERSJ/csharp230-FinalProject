using System.Linq;

namespace LearningCenter.Repository
{
    public interface IClassRepository
    {
        ClassModel[] Classes { get; }
        ClassModel[] StudentClasses(int[] classIds);

        ClassModel Class(int classId);
    }

    public class ClassModel
    {
        public ClassModel()
        {

        }

        public ClassModel(int id, string name, string descrption, decimal price)
        {
            Id = id;
            Name = name;
            Description = descrption;
            Price = price;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class ClassRepository : IClassRepository
    {
        public ClassModel[] Classes
        {
            get
            {
                ClassModel[] result = DatabaseAccessor.Instance.Classes
                                               .Select(t => new ClassModel { Id = t.ClassId, Name = t.ClassName, Description = t.ClassDescription, Price = t.ClassPrice })
                                               .ToArray();

                return result;
            }
        }


        public ClassModel Class(int classId)
        {
            var offeredClass = DatabaseAccessor.Instance.Classes
                                                   .Where(t => t.ClassId == classId)
                                                   .Select(t => new ClassModel { Id = t.ClassId, Name = t.ClassName, Description = t.ClassDescription, Price = t.ClassPrice })
                                                   .First();
            return offeredClass;
        }



        public ClassModel[] StudentClasses(int[] classIds)
        {
            var offeredClass = DatabaseAccessor.Instance.Classes
                                                   .Where(t => classIds.Contains(t.ClassId))
                                                   .Select(t => new ClassModel { Id = t.ClassId, Name = t.ClassName, Description = t.ClassDescription, Price = t.ClassPrice })
                                                   .ToArray();
            return offeredClass;
        }
    }
}