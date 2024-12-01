using nuint.queries;
using nunit.dto;
using nunit.errors;
using nunit.models;

namespace nunit.service
{
    public interface IUserService
    {
        User Signup(SignupRequest request);
        User UserData(string email);
        List<User> AllUsers();
        User UpdateUserDetails(string email, string firstName, string lastName);
        User DeleteAccount(string email);
    }

    public class UserService(IUserQueries userQueries) : IUserService
    {
        private readonly IUserQueries _userQueries = userQueries;

        public User Signup(SignupRequest req)
        {
            var existingUser = _userQueries.GetUser(req.Email);

            if (existingUser != null)
            {
                throw new NunitApiException(403, "User Already Exists");
            }

            var user =
                _userQueries.CreateUser(req.Email, req.FirstName, req.LastName)
                ?? throw new NunitApiException(500, "Error while creating Signing Up User");

            return user;
        }

        public User UserData(string email)
        {
            throw new NotImplementedException();
        }

        public List<User> AllUsers()
        {
            throw new NotImplementedException();
        }

        public User UpdateUserDetails(string email, string firstName, string lastName)
        {
            throw new NotImplementedException();
        }

        public User DeleteAccount(string email)
        {
            throw new NotImplementedException();
        }
    }
}
