using FootballApi.Application.Dtos;
using FootballApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FootballApi.WebUI1.Controllers
{
    public class AdminStadiumController : Controller
    {
        private readonly IStadiumService _stadiumService;

        public AdminStadiumController(IStadiumService stadiumService)
        {
            _stadiumService = stadiumService;
        }

        public async Task<IActionResult> Index()
        {
            var stadiums = await _stadiumService.GetAllAsync();
            return View(stadiums);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StadiumDto stadiumDto)
        {
            if (ModelState.IsValid)
            {
                await _stadiumService.AddAsync(stadiumDto);
                return RedirectToAction("Index");
            }
            return View(stadiumDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var stadium = await _stadiumService.GetByIdAsync(id);
            if (stadium == null) return NotFound();

            return View(stadium);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StadiumDto stadiumDto)
        {
            if (ModelState.IsValid)
            {
                await _stadiumService.UpdateAsync(stadiumDto);
                return RedirectToAction("Index");
            }
            return View(stadiumDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _stadiumService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
