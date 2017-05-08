using System.Collections.Generic;
using System.Linq;
using LearningCenter.ProductDatabase;

namespace LearningCenter.Repository
{
    public interface IUserRepository
    {
        UserModel LogIn(string email, string password);
        UserModel Register(string email, string password);
        void Enroll(int userId, int classId);

    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ClassModel[] Classes { get; set; }
    }

    public class UserRepository : IUserRepository
    {
        public UserModel LogIn(string email, string password)
        {
            var user = DatabaseAccessor.Instance.Users
                .FirstOrDefault(t => t.UserEmail.ToLower() == email.ToLower()
                                      && t.UserPassword == password);

            if (user == null)
            {
                return null;
            }

            return new UserModel { Id = user.UserId, Name = user.UserEmail, Classes = user.Classes?.Select(c => new ClassModel(c.ClassId, c.ClassName, c.ClassDescription, c.ClassPrice)).ToArray() };
        }

        public UserModel Register(string email, string password)
        {
            var user = DatabaseAccessor.Instance.Users
                    .Add(new LearningCenter.ProductDatabase.User { UserEmail = email, UserPassword = password });

            DatabaseAccessor.Instance.SaveChanges();

            return new UserModel { Id = user.UserId, Name = user.UserEmail };
        }


        public void Enroll(int userId, int classId)
        {
            var user = DatabaseAccessor.Instance.Users
                    .FirstOrDefault(u => u.UserId == userId);

            var cls = DatabaseAccessor.Instance.Classes
                    .FirstOrDefault(c => c.ClassId == classId);

            user.Classes.Add(cls);
            DatabaseAccessor.Instance.SaveChanges();

        }
    }
}
