using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Api.Model.Authentication.Interfaces;
using MiniShop.Api.Model.DTO;
using MiniShop.Api.Model.Present;
using MiniShop.Core.DTO;
using MiniShop.Core.DTO.Request;
using MiniShop.Core.DTO.Response;
using MiniShop.Core.Interfaces.Repositories;
using MiniShop.Core.UseCases;

namespace MiniShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _adminRepository;
        private readonly IAuthenticationManager _authenticationManager;

        public AccountController(IUserRepository adminRepository,
            IAuthenticationManager authenticationManager)
        {
            _adminRepository = adminRepository;
            _authenticationManager = authenticationManager;
        }

        [HttpPost("CheckAuthData")]
        public async Task<IActionResult> CheckAuthData(LoginVM loginVM)
        {
            try
            {
                var checkAuthDataUseCase = new CheckAuthDataUseCase(_adminRepository, new Presenter<LoginResponse>());
                var result = await checkAuthDataUseCase.HandleAsync(new LoginRequest(loginVM.Username, loginVM.Password));

                if (result.Status)
                    result.Data.Token = _authenticationManager.GetToken(loginVM.Username, result.Data.Roles.ToArray());

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterVM registerVM)
        {
            try
            {
                var registerUseCase = new RegisterUseCase(_adminRepository, new Presenter<bool>());
                var result = await registerUseCase.HandleAsync(new RegisterRequest(registerVM.FirstName, registerVM.LastName, registerVM.Username, registerVM.Password));
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }

        [HttpPost("GetAllUsers")]
        [Authorize(Policy = Role.PowerUser)]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var getAllUsersUseCase = new GetAllUsersUseCase(_adminRepository, new Presenter<List<GetAllUsersReponse>>());
                var result = await getAllUsersUseCase.HandleAsync();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }

        [HttpPost("ApproveUser")]
        [Authorize(Policy = Role.PowerUser)]
        public async Task<IActionResult> ApproveUser(ApproveUserVM approveUserVM)
        {
            try
            {
                var getAllUsersUseCase = new ApproveUserUseCase(_adminRepository, new Presenter<bool>());
                var result = await getAllUsersUseCase.HandleAsync(approveUserVM.UserId);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }

        [HttpGet("AccessDenied")]
        [HttpPost("AccessDenied")]
        public IActionResult AccessDenied()
        {
            var presenter = new Presenter<string>();
            presenter.PresenterFail("You don’t currently have permission to access this section!");
            return Ok(presenter);
        }
    }
}
