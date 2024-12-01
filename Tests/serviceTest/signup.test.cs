using Moq;
using nuint.queries;
using nunit.dto;
using nunit.errors;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using nunit.models;
using nunit.service;

namespace nunit.test.service
{
    [TestFixture]
    public class UserServiceTest
    {
        private Mock<IUserQueries>? _mockUserQueries;
        private IUserService? _userService;

        [SetUp]
        public void SetUp()
        {
            _mockUserQueries = new Mock<IUserQueries>();

            _userService = new UserService(_mockUserQueries.Object);
        }

        [Test]
        public void Signup_ReturnsUser_UserDoesNotExists()
        {
            var signupRequest = new SignupRequest
            {
                Email = "test@Email.com",
                FirstName = "firstName",
                LastName = "LastName",
            };

            var newUser = new User
            {
                Email = signupRequest.Email,
                FirstName = signupRequest.FirstName,
                LastName = signupRequest.LastName,
            };

            _mockUserQueries?.Setup(q => q.GetUser(signupRequest.Email)).Returns((User?)null);

            _mockUserQueries
                ?.Setup(q =>
                    q.CreateUser(
                        signupRequest.Email,
                        signupRequest.FirstName,
                        signupRequest.LastName
                    )
                )
                .Returns(newUser);

            var result = _userService?.Signup(signupRequest);

            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(newUser.Email, result?.Email);
            ClassicAssert.AreEqual(newUser.FirstName, result?.FirstName);
            ClassicAssert.AreEqual(newUser.LastName, result?.LastName);
        }

        [Test]
        public void Signup_ThrowError_UserExists()
        {
            var signupRequest = new SignupRequest
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
            };

            var existingUser = new User
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
            };

            _mockUserQueries?.Setup(q => q.GetUser(signupRequest.Email))?.Returns(existingUser);

            var ex = Assert.Throws<NunitApiException>(() => _userService?.Signup(signupRequest));

            Assert.That(ex?.StatusCode, Is.EqualTo(403));
            Assert.That(ex?.Message, Is.EqualTo("User Already Exists"));
        }
    }
}
