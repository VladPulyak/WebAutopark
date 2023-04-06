using DataLayer.Entities;
using DataLayer.Repositories.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAutopark.Controllers
{
    public class ComponentsController : Controller
    {
        private readonly IComponentsRepository _componentsRepository;
        public ComponentsController(IComponentsRepository componentsRepository)
        {
            _componentsRepository = componentsRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddComponent(Components component)
        {
            await _componentsRepository.Add(component);
            return RedirectToAction("GetAllComponents", "Components");
        }

        [HttpGet]
        public IActionResult AddComponent()
        {
            return View();
        }
        public async Task<IActionResult> DeleteComponentById(int componentId)
        {
            await _componentsRepository.Delete(componentId);
            return RedirectToAction("GetAllComponents", "Components");
        }
        public async Task<IActionResult> GetAllComponents()
        {
            var listWithComponents = await _componentsRepository.GetAll();
            return View(listWithComponents);
        }
        [HttpPost]
        public async Task<IActionResult> GetComponentById(int componentId)
        {
            var component = await _componentsRepository.GetById(componentId);
            return View(component);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateComponent(int componentId)
        {
            var component = await _componentsRepository.GetById(componentId);
            return View(component);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateComponent(Components component)
        {
            await _componentsRepository.Update(component.ComponentId, component);
            return RedirectToAction("GetAllComponents", "Components");
        }
    }
}
