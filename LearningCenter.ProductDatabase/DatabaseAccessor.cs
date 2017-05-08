using LearningCenter.ProductDatabase;

namespace LearningCenter.Repository
{
    public class DatabaseAccessor
    {
        private static readonly MiniCstructorDbEntities entities;

        static DatabaseAccessor()
        {
            entities = new MiniCstructorDbEntities();
            entities.Database.Connection.Open();
        }

        public static MiniCstructorDbEntities Instance
        {
            get
            {
                return entities;
            }
        }
    }
}