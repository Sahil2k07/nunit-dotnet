using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nunit.dto;
using nunit.errors;
using nunit.service;

namespace nuint.controller
{
    [Controller]
    [Route("/api/user")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [AllowAnonymous]
        [HttpPost("signup")]
        public IActionResult Signup([FromBody] SignupRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(
                    new
                    {
                        success = false,
                        message = "Invalid Payload",
                        errors,
                    }
                );
            }

            try
            {
                var user = _userService.Signup(request);

                return Ok(
                    new
                    {
                        Success = true,
                        Message = "Signup Successfull",
                        Data = user,
                    }
                );
            }
            catch (NunitApiException ex)
            {
                return StatusCode(ex.StatusCode, new { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    500,
                    new
                    {
                        Success = false,
                        Message = "Someting Unexpected",
                        Error = ex.Message,
                    }
                );
            }
        }
    }
}
