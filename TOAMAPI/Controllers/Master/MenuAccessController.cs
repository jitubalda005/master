using Domain.Entities.Master;
using Domain.Interfaces;
using Domain.Interfaces.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlowvidDevAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuAccessController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public MenuAccessController(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }



        [HttpGet("GetMenusAccess")]
        public async Task<IActionResult> GetMenusAccess()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            var menuAccessRepository = _unitOfWork.GetRepository<IMenuAccessRepository>();
            var MenusAccess = await menuAccessRepository.GetUserMenuAccess(user.UserName);
            return Ok(MenusAccess);
        }

        [HttpGet("GetUnAccessMenus")]
        public async Task<IActionResult> GetUnAccessMenus()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            var menuAccessRepository = _unitOfWork.GetRepository<IMenuAccessRepository>();
            var MenusAccess = await menuAccessRepository.GetUnAccessMenus(user.UserName);
            return Ok(MenusAccess);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult> GetMenuAccessByID(int id)
        {

            var menuAccessRepository = _unitOfWork.GetRepository<IMenuRepository>();
            var MenusAccess = await menuAccessRepository.GetByIdAsync(id);
            if (MenusAccess == null)
            {
                return NotFound();
            }
            return Ok(MenusAccess);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMenuAccess([FromBody] MenuAccess menuAccess)
        {
            var menuAccessRepository = _unitOfWork.GetRepository<IMenuAccessRepository>();
            await menuAccessRepository.AddAsync(menuAccess);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetMenuAccessByID), new { id = menuAccess.Id }, menuAccess);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenusAccess(int id, [FromBody] MenuAccess menuAccess)
        {
            if (id != menuAccess.Id)
            {
                return BadRequest();
            }

            var menuAccessRepository = _unitOfWork.GetRepository<IMenuAccessRepository>();
            menuAccessRepository.Update(menuAccess);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuAccess(int id)
        {
            var menuAccessRepository = _unitOfWork.GetRepository<IMenuAccessRepository>();
            var menuAccess = await menuAccessRepository.GetByIdAsync(id);
            if (menuAccess == null)
            {
                return NotFound();
            }

            menuAccessRepository.Remove(menuAccess);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
