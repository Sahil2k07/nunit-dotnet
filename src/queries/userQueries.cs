using nunit.db;
using nunit.models;

namespace nuint.queries
{
    public interface IUserQueries
    {
        User? CreateUser(string email, string firstName, string lastName);
        List<User> GetAllUsers(int pageNumber, int pageSize);
        User? GetUser(string email);
        User? DeleteUser(string email);
        User? UpdateUser(string email, string firstName, string LastName);
    }

    public class UserQueries(NunitDbContext dbContext) : IUserQueries
    {
        private readonly NunitDbContext _dbContext = dbContext;

        public User? CreateUser(string email, string firstName, string LastName)
        {
            var user = new User
            {
                Email = email,
                FirstName = firstName,
                LastName = LastName,
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user;
        }

        public List<User> GetAllUsers(int pageNumber, int pageSize)
        {
            var users = _dbContext.Users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return users;
        }

        public User? GetUser(string email)
        {
            var user = _dbContext.Users.Where(u => u.Email == email).FirstOrDefault();

            return user;
        }

        public User? UpdateUser(string email, string firstName, string lastName)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                user.FirstName = firstName;
                user.LastName = lastName;

                _dbContext.Users.Update(user);
                _dbContext.SaveChanges();
            }

            return user;
        }

        public User? DeleteUser(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
                return user;

            _dbContext.Remove(user);
            _dbContext.SaveChanges();

            return user;
        }
    }
}
