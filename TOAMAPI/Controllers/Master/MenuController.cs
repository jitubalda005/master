using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities.Master;
using DataAccess.EFCore.Repositories;
using Domain.Interfaces.Master;

namespace FlowvidDevAPI.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public MenuController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> GetMenus()
        {
            var menuRepository = _unitOfWork.GetRepository<IMenuRepository>();
            var menus = await menuRepository.GetAllAsync();
            return Ok(menus);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetMenuByID(int id)
        {

            var menuRepository = _unitOfWork.GetRepository<IMenuRepository>();
            var menus = await menuRepository.GetByIdAsync(id);
            if (menus == null)
            {
                return NotFound();
            }
            return Ok(menus);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMenu([FromBody] Menu menu)
        {
            var menuRepository = _unitOfWork.GetRepository<IMenuRepository>();
            await menuRepository.AddAsync(menu);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetMenuByID), new { id = menu.ID }, menu);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenu(int id, [FromBody] Menu menu)
        {
            if (id != menu.ID)
            {
                return BadRequest();
            }

            var menuRepository = _unitOfWork.GetRepository<IMenuRepository>();
            menuRepository.Update(menu);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var menuRepository = _unitOfWork.GetRepository<IMenuRepository>();
            var menu = await menuRepository.GetByIdAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            menuRepository.Remove(menu);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
