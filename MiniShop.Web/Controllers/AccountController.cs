using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Core.DTO.Request;
using MiniShop.Core.DTO.Response;
using MiniShop.Core.Interfaces.Repositories;
using MiniShop.Core.UseCases;
using MiniShop.Web.DTO;
using MiniShop.Web.Present;

namespace MiniShop.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;

        public AccountController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        [HttpPost("CheckAuthData")]
        public async Task<IActionResult> CheckAuthData(LoginVM loginVM)
        {
            try
            {
                var checkAuthDataUseCase = new CheckAuthDataUseCase(_adminRepository, new Presenter<LoginResponse>());
                var result = await checkAuthDataUseCase.HandleAsync(new LoginRequest(loginVM.Username, loginVM.Password));
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
        public async Task<IActionResult> GetAllUsers(TokenVM request)
        {
            try
            {
                var getAllUsersUseCase = new GetAllUsersUseCase(_adminRepository, new Presenter<List<GetAllUsersReponse>>());
                var result = await getAllUsersUseCase.HandleAsync(request.Token);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }

        [HttpPost("ApproveUser")]
        public async Task<IActionResult> ApproveUser(ApproveUserVM approveUserVM)
        {
            try
            {
                var getAllUsersUseCase = new ApproveUserUseCase(_adminRepository, new Presenter<bool>());
                var result = await getAllUsersUseCase.HandleAsync(new ApproveUserRequest(approveUserVM.Token, approveUserVM.UserToken));
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Internal server error");
            }
        }
    }
}
