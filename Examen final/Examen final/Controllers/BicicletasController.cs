using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Examen_final.Models;
using Examen_final.Services;
using Microsoft.AspNetCore.Authorization;

namespace Examen_final.Controllers
{
    public class BicicletasController : Controller
    {
        // GET: BicicletasController
        [Authorize]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Bicicleta> bicicleta = await ApiService.GetBicicleta();
            return View(bicicleta);
        }

        // GET: BicicletasController/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var bicicleta = await ApiService.BicicletaDetails(id);
            return View(bicicleta);
        }

        // GET: BicicletasController/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: BicicletasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Bicicleta bicicleta, IFormFile Imagen)
        {

            try
            {
                if (Imagen != null && Imagen.Length > 0)
                {
                    bicicleta.Imagen = SaveImage(Imagen);
                }
                await ApiService.CreateBicicleta(bicicleta);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BicicletasController/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var bicicleta = await ApiService.BicicletaDetails(id);
            return View(bicicleta);
        }

        // POST: BicicletasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, Bicicleta bicicleta, IFormFile Imagen)
        {
            try
            {
                if (Imagen != null && Imagen.Length > 0)
                {
                    bicicleta.Imagen = SaveImage(Imagen);
                }
                await ApiService.UpdateBicicleta(bicicleta);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BicicletasController/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var bicicleta = await ApiService.BicicletaDetails(id);
            return View(bicicleta);
        }

        // POST: BicicletasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Delete(int id, Bicicleta collection)
        {
            try
            {
                await ApiService.DeleteBicicleta(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private string SaveImage(IFormFile file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imagen", fileName);
            if (System.IO.File.Exists(filePath))
            {
                int i = 1;
                string fileNameOnly = Path.GetFileNameWithoutExtension(filePath);
                string extension = Path.GetExtension(filePath);
                while (System.IO.File.Exists(filePath))
                {
                    fileName = string.Format("{0}({1})", fileNameOnly, i++);
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imagen", fileName + extension);
                }
            }
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(stream).Wait();
            }
            return fileName;
        }
    }
}
