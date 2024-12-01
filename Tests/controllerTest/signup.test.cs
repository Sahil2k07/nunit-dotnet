using Microsoft.AspNetCore.Mvc;
using Moq;
using nuint.controller;
using nunit.dto;
using nunit.errors;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using nunit.models;
using nunit.service;

namespace nunit.test.controller
{
    [TestFixture]
    public class UserControllerTest
    {
        private Mock<IUserService>? _mockUserService;
        private UserController? _userController;

        [SetUp]
        public void SetUp()
        {
            _mockUserService = new Mock<IUserService>();

            _userController = new UserController(_mockUserService.Object);
        }

        [Test]
        public void Signup_ReturnsOk_WhenUserIsCreated()
        {
            var signupRequest = new SignupRequest
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
            };

            var newUser = new User
            {
                Email = signupRequest.Email,
                FirstName = signupRequest.FirstName,
                LastName = signupRequest.LastName,
            };

            _mockUserService?.Setup(s => s.Signup(signupRequest)).Returns(newUser);

            var result = _userController?.Signup(signupRequest);

            var okResult = result as OkObjectResult;
            ClassicAssert.NotNull(okResult);

            var response = okResult?.Value as dynamic;

            ClassicAssert.AreEqual("Signup Successfull", response?.Message);
            ClassicAssert.AreEqual(newUser.Email, response?.Data?.Email);
            ClassicAssert.AreEqual(newUser.FirstName, response?.Data?.FirstName);
            ClassicAssert.AreEqual(newUser.LastName, response?.Data?.LastName);
        }

        [Test]
        public void Signup_ReturnsBadRequest_WhenRequestIsInvalid()
        {
            var signupRequest = new SignupRequest
            {
                Email = "invalidemail",
                FirstName = "",
                LastName = "",
            };

            _userController!.ModelState.AddModelError("Email", "Invalid Email");
            _userController.ModelState.AddModelError("FirstName", "FirstName Required");
            _userController.ModelState.AddModelError("LastName", "LastName Required");

            var result = _userController?.Signup(signupRequest);

            var badRequestResult = result as BadRequestObjectResult;
            ClassicAssert.IsNotNull(badRequestResult);
        }

        [Test]
        public void Signup_ReturnsForbidden_WhenUserAlreadyExists()
        {
            var signupRequest = new SignupRequest
            {
                Email = "existing@example.com",
                FirstName = "John",
                LastName = "Doe",
            };

            var existingUser = new User
            {
                Email = signupRequest.Email,
                FirstName = signupRequest.FirstName,
                LastName = signupRequest.LastName,
            };

            _mockUserService
                ?.Setup(s => s.Signup(signupRequest))
                .Throws(new NunitApiException(403, "User Already Exists"));

            var result = _userController?.Signup(signupRequest);

            var forbiddenResult = result as ObjectResult;
            ClassicAssert.IsNotNull(forbiddenResult);

            var response = forbiddenResult?.Value as dynamic;

            ClassicAssert.AreEqual(403, forbiddenResult?.StatusCode);
            ClassicAssert.AreEqual("User Already Exists", response?.Message);
        }

        [Test]
        public void Signup_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            var signupRequest = new SignupRequest
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
            };

            _mockUserService
                ?.Setup(s => s.Signup(signupRequest))
                .Throws(new Exception("Unexpected error"));

            var result = _userController?.Signup(signupRequest);

            var statusCodeResult = result as ObjectResult;
            ClassicAssert.IsNotNull(statusCodeResult);

            var response = statusCodeResult?.Value as dynamic;

            ClassicAssert.AreEqual(500, statusCodeResult?.StatusCode);
            ClassicAssert.AreEqual("Someting Unexpected", response?.Message);
            ClassicAssert.AreEqual("Unexpected error", response?.Error);
        }
    }
}
