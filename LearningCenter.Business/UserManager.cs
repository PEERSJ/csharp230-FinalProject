using System.Linq;
using LearningCenter.Repository;


namespace LearningCenter.Business
{
    public interface IUserManager
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

    public class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserModel LogIn(string email, string password)
        {
            var user = userRepository.LogIn(email, password);

            if (user == null)
            {
                return null;
            }

            return new UserModel { Id = user.Id, Name = user.Name, Classes = user.Classes?.Select(c => new ClassModel(c.Id, c.Name, c.Description, c.Price)).ToArray() };
        }

        public UserModel Register(string email, string password)
        {
            var user = userRepository.Register(email, password);

            if (user == null)
            {
                return null;
            }

            return new UserModel { Id = user.Id, Name = user.Name, Classes = user.Classes?.Select(c => new ClassModel(c.Id, c.Name, c.Description, c.Price)).ToArray() };
        }



        public void Enroll(int userId, int classId)
        {
            userRepository.Enroll(userId, classId);
        }
    }
}
