using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities.Master;
using DataAccess.EFCore.Repositories;

namespace FlowvidDevAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public LoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> GetMenus()
        {
            var userManagmentRepository = _unitOfWork.GetRepository<IUserManagmentRepository>();
            var menus = await userManagmentRepository.GetAllAsync();
            return Ok(menus);
        }

     
        [HttpPost("[action]")]
        public async Task<ActionResult> LoginCredential([FromBody] UserManagment loginModel)
        {
            var userManagmentRepository = _unitOfWork.GetRepository<IUserManagmentRepository>();
            var mytask = Task.Run(() => userManagmentRepository.LoginCredential(loginModel.Type, loginModel.Credential));
            var result = await mytask;
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

           
        //[HttpPost("[action]")]
        //public async Task<IActionResult> LoginCheck([FromBody] LoginRequestBM loginModel)
        //{
        //    IActionResult response = Unauthorized();

        //    string unqueid = "" + HttpContext.Connection.LocalIpAddress + HttpContext.Connection.LocalPort + HttpContext.Connection.RemoteIpAddress;
        //    var mytask = Task.Run(() => repository.Login(loginModel.Username, loginModel.Password));
        //    var result = await mytask;
        //    //var user = AuthenticateUser1(loginModel);
        //    if (result.Count > 0)
        //    {
        //        var tokenString = GenerateJSONWebToken(result[0]);
        //        var tokentask = Task.Run(() => repository.AuthorizeTokanStore(result[0].UserId, tokenString, unqueid, loginModel.DeviceType));

        //        response = Ok(new { token = tokenString, Status = true, Result = result });
        //    }
        //    else
        //    {
        //        response = Ok(new { Status = false });
        //    }
        //    return response;
        //}

        //[ActionFilter]
        //[HttpPost("[action]")]
        //public async Task<IActionResult> LogOff([FromBody] string userid)
        //{
        //    IActionResult response = Unauthorized();
        //    var mytask = Task.Run(() => repository.LogOff(userid));
        //    var result = await mytask;
        //    if (result == "success")
        //    {

        //        response = Ok(new { Result = "success" });
        //    }
        //    else
        //    {
        //        response = Ok(new { Result = "failure" });
        //    }
        //    return response;
        //}

        //private string GenerateJSONWebToken(LoginResponseBM userInfo)
        //{

        //    try
        //    {
        //        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //        var claims = new[] {
        //        new Claim("id", ""+userInfo.UserId),
        //        new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
        //        new Claim(JwtRegisteredClaimNames.Email, userInfo.Password),
        //        //new Claim("DateOfJoing", userInfo.DateOfJoing.ToString("yyyy-MM-dd")),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //    };
        //        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //        _config["Jwt:Issuer"],
        //        claims,
        //        expires: DateTime.Now.AddMinutes(120),
        //        signingCredentials: credentials);
        //        return new JwtSecurityTokenHandler().WriteToken(token);
        //    }
        //    catch (Exception e)
        //    {
        //    }
        //    return null;
        //}

    }
}
