using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prk1.Dal;
using Prk1.Models;
using Prk1.ViewModels;

namespace Prk1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<ActionResult> Index()
        {

            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            return View(products);
        }
        public async Task<IActionResult> Create()
        {
            
            CreateProductVM productVM = new CreateProductVM
            {
                Categories = await _context.Categories.ToListAsync()
            };
            return View(productVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM productVM)
        {
            if (!ModelState.IsValid)
            {
                productVM.Categories = await _context.Categories.ToListAsync();
                return View(productVM);
            }
            string filename = Guid.NewGuid().ToString() + productVM.Photo.FileName;

            string path = Path.Combine(_env.WebRootPath, "admin", "images", "products", filename);
            using (FileStream file = new FileStream(path, FileMode.Create))
            {
                await productVM.Photo.CopyToAsync(file);

            };


            await _context.Products.AddAsync(new Product
            {
                CategoryId = productVM.CategoryId,
                Image = filename,
                Name = productVM.Name 
            });

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 0) BadRequest();

            var data = _context.Products.FirstOrDefault(p => p.Id == id);

            if (data == null) NotFound();


            UpdateProductVM vM = new UpdateProductVM
            {
                Name = data.Name,
                Categories = await _context.Categories.ToListAsync()

            };

            return View(vM);

        }
        
        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateProductVM vm)
        {
            if (id == null || id < 0) BadRequest();

            var data = _context.Products.FirstOrDefault(p => p.Id == id);

            if (data == null) NotFound();

            if (!ModelState.IsValid)
            {
                vm.Categories = await _context.Categories.ToListAsync();
                return View(vm);
            }

            string filename = Guid.NewGuid().ToString() + vm.Photo.FileName;

            string path = Path.Combine(_env.WebRootPath, "admin", "images", "products", filename);
            using (FileStream file = new FileStream(path, FileMode.Create))
            {
                await vm.Photo.CopyToAsync(file);

            };

            data.Name = vm.Name;
            data.CategoryId = vm.CategoryId;
            data.Image = filename;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 0) BadRequest();

            var data = _context.Products.FirstOrDefault(p => p.Id == id);

            if (data == null) NotFound();

            _context.Products.Remove(data);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


        }

}
