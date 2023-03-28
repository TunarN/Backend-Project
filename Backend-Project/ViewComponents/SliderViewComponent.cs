using Backend_Project.DAL;
using Backend_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Project.ViewComponents
{
    public class SliderViewComponent:ViewComponent
    {

        private readonly AppDbContext _appDbContext;

        public SliderViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Student student = _appDbContext.Students.FirstOrDefault();
            return View(await Task.FromResult(student));
        }
    }
}
